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
using ReflectionIT.Mvc.Paging;
using SQLitePCL;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    //Worked by Daniel Tejada
    [Area("Admin")]
    public class GestionProductos : Controller
    {
        readonly DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();

        [HttpGet]
        //get the specific product
        public async Task<IActionResult> Index(string valor, int page = 1)
        {
            //PagedResult<Producto> result = null;
            ViewData["ValueSearch"] = valor;
            List<Producto> productos;
            if (!String.IsNullOrEmpty(valor))
            {
                //Seacrh product
                productos = await _context.Producto.Include(p => p.IdCategoriaNavigation).
                    Where(p => p.Nombre.Contains(valor) || p.IdCategoriaNavigation.Nombre.Contains(valor)).
                    OrderByDescending(P=> P.IdProducto)
                .AsNoTracking().ToListAsync();
                //paginator
                var model = PagingList.Create(productos, 6, page);
                return View(model);
            }
            else
            {
                productos = await _context.Producto.Include(t => t.IdCategoriaNavigation).
                    OrderByDescending(P => P.IdProducto).
                AsNoTracking().ToListAsync();
                var model = PagingList.Create(productos, 6, page);
                return View(model);
            }
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
        public async Task<IActionResult> Edit([Bind("IdProducto,Nobre,Descripcion,Precio,Foto,Idcategoria")] Producto producto)
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
        public async Task<IActionResult> DeleteCornfirmed(int IdProducto)
        {
            var producto = await _context.Producto.FindAsync(IdProducto);
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
