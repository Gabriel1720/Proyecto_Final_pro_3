using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    //Worked by Daniel Tejada
    [Area("Admin")]
    public class GestionProductos : Controller
    {
        readonly DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();
        
        //Get list of Productos
        public async Task<IActionResult> Index()
        {
            var products = await _context.Producto.Include(T =>T.IdCategoriaNavigation).ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        //Get view
        public IActionResult Create()
        {
            ViewData["CategoriaNombre"] = new SelectList(_context.Categoria, "IdCategoria", "Nombre");
            return View();
        }

        //Post Products
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,precio,")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaNombre"] = new SelectList(_context.Categoria, "IdCategoria", "Nombre", 
                producto.IdCategoria);
            return View(producto);
        }

        //Get Details about that 
        public async Task<IActionResult>Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            //table productos and categoria
            Producto producto = await _context.Producto.Include(T=> T.IdCategoriaNavigation).
                FirstOrDefaultAsync(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
    }
}
