using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_final_pro_3.Models;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GestionReportes : Controller
    {
        DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();

        public async Task<IActionResult> Index()
        {
            var orden = await _context.Orden.Include(x => x.IdUsuarioNavigation).ToListAsync();
            return View(orden); 
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.order = await _context.Orden.Include(x => x.IdUsuarioNavigation).Include(X => X.DetalleOrden).FirstOrDefaultAsync(x => x.IdOrden == id);
            ViewBag.Detalle_orden = await _context.DetalleOrden.Include(x => x.IdProductoNavigation).Where(x => x.IdOrden == id).ToListAsync();
            return View();
        }
    }
}
