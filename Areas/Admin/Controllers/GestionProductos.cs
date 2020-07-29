using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            //table productos and categoria
            var products = await _context.Producto.Include(T =>T.IdCategoriaNavigation).ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
         [HttpPost]
        public async Task<IActionResult> Search(string valor)
        {

            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,Precio,Foto,IdCategoria")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

        //Get view of product 
        public async Task<IActionResult> Edit(int? id) 
        {
            if(id == null)
            {
                return NotFound();
            }
            var producto = await  _context.Producto.FindAsync(id); 
            if(producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaNombre"] = new SelectList(_context.Categoria,"IdCategoria","Nombre", producto.IdCategoria);
            return View(producto);
        }

        //Edit done by post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Nobre,Descripcion,Precio,Foto,Idcategoria")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if  (!ProductoExists(producto.IdProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaNombre"] = new SelectList(_context.Categoria, "IdCategoria", "Nombre", producto.IdCategoria);
            return View(producto);
        }

        //Get product for delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.Include(c => c.IdCategoriaNavigation).
                FirstOrDefaultAsync(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        //Post delete
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteCornfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Remove(producto);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        


        //if curreent user exist, validate that
        private  bool ProductoExists(int id) =>  
             _context.Producto.Any(p => p.IdProducto == id);
    }
    
}
