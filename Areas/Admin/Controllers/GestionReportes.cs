using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_final_pro_3.Models;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GestionReportes : Controller
    {
        DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();

        public async Task<IActionResult> Index(string srt = "", int page = 1)
        {
            IOrderedQueryable<Orden> orden = null;    

            if (!String.IsNullOrEmpty(srt))
            {
                Guardar.Srt = srt;
                orden = _context.Orden.AsNoTracking().Include(x => x.IdUsuarioNavigation)
                .Where(x => x.IdUsuarioNavigation.Nombre == Guardar.Srt || x.IdUsuarioNavigation.Correo == Guardar.Srt)
                .OrderBy(x => x.IdOrden);

            }
            else if (String.IsNullOrEmpty(srt) || String.IsNullOrEmpty(Guardar.Srt))
            {
                orden = _context.Orden.AsNoTracking().Include(x => x.IdUsuarioNavigation).OrderBy(x => x.IdOrden);
                Guardar.Srt = null;
            }     

            ViewBag.srt = Guardar.Srt;
            var model = await PagingList.CreateAsync(orden, 7, page);            
            return View(model); 
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.order = await _context.Orden.Include(x => x.IdUsuarioNavigation).Include(X => X.DetalleOrden).FirstOrDefaultAsync(x => x.IdOrden == id);
            ViewBag.Detalle_orden = await _context.DetalleOrden.Include(x => x.IdProductoNavigation).Where(x => x.IdOrden == id).ToListAsync();
            ViewBag.Total = await _context.DetalleOrden.Include(x => x.IdProductoNavigation).Where(x => x.IdOrden == id).SumAsync(x => x.Precio);
                      
            return View();
        }
    }
}
