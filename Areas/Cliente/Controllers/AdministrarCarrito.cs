using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class AdministrarCarrito : Controller
    {
        DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();

        public async Task<IActionResult> Index()
        {
            string idUser = HttpContext.Session.GetString("userID");
            int id = Int32.Parse(idUser);
            var carrito = await _context.Carrito.Include(x => x.IdProductoNavigation).Where(x => x.IdUsuario == id).ToListAsync();
            ViewBag.Total = await _context.Carrito.Where(x => x.IdUsuario == 47)
                            .SumAsync(x => x.Cantidad * x.IdProductoNavigation.Precio);         
            
            return View(carrito);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var carrito = await _context.Carrito.FindAsync(id);
            _context.Remove(carrito);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
