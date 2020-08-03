using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Proyecto_final_pro_3.Models;
using ReflectionIT.Mvc.Paging;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GestionPedidosController : Controller
    {
        readonly DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();

        [HttpGet]
        public async Task< IActionResult> Index(string PedidoBuscar, int page=1)
        {
            List<Orden>pedidos;
            dynamic model;
            //for search pedidos
            if (!String.IsNullOrEmpty(PedidoBuscar))
            {
                Guardar.PedidoBucar = PedidoBuscar;
                pedidos = await SearhPedido(PedidoBuscar);                            
            }
            else
            {
                Guardar.PedidoBucar = "";
                pedidos = await GetAllPedidos();               
            }
            model = PagingList.Create(pedidos, 5, page);
            if (pedidos == null)
            {
                return NotFound();
            }
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPedidosMap()
        {
            List<Orden> pedidos = await GetAllPedidos();
           
            if(pedidos == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(_context.StatusOrden, "IdStatusOrden", "Nombre");
            return View(pedidos);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pedido = await FindFirstOrden(id);
            var detalleOrden = await _context.DetalleOrden.Where(d => d.IdOrden == id).
                Include(d=> d.IdProductoNavigation).
                ToListAsync();
            if (pedido == null || detalleOrden == null)
            {
                return NotFound();
            }
            double? total = 0;
            //Total purchase
            foreach (var i in detalleOrden)
            {
                total = total + i.Precio * i.Cantidad;
            }
            ViewData["detalleOrdenTotal"] = total;
            ViewData["detalleOrden"] = detalleOrden;
            return View(pedido);
        }

        [HttpGet]
        public async Task<IActionResult> Manage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pedido = await FindFirstOrden(id);
            if(pedido == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(_context.StatusOrden, "IdStatusOrden", "Nombre");
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(Orden orden)
        {
            if (orden.IdStatusOrden!=null)
            {               
                _context.Entry(orden).Property(o => o.IdStatusOrden).IsModified = true;              
               await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }



        //-----------------------------------------Query section------------------------------------------------------------------


        private async Task<List<Orden>> SearhPedido(string PedidoBuscar)
        {
            List<Orden> pedidos;
            pedidos = await _context.Orden.Include(o => o.IdStatusOrdenNavigation).
                Include(o => o.IdDomicilioNavigation).Include(o => o.IdUsuarioNavigation).
                Where(u => u.IdUsuarioNavigation.Nombre.Contains(PedidoBuscar) ||
                u.IdUsuarioNavigation.Correo.Contains(PedidoBuscar) ||
                u.IdStatusOrdenNavigation.Nombre.Contains(PedidoBuscar) ||
                u.IdUsuarioNavigation.Apellido.Contains(PedidoBuscar)).
                OrderByDescending(o => o.IdOrden).
                 AsNoTracking().ToListAsync();
            return pedidos;
        }


        private async Task<List<Orden>> GetAllPedidos()
        {
            List<Orden> pedidos;
            pedidos = await _context.Orden.Include(o => o.IdStatusOrdenNavigation).
                   Include(o => o.IdDomicilioNavigation).Include(o => o.IdUsuarioNavigation).
                   OrderByDescending(o => o.IdOrden).ToListAsync();
            return pedidos;
        }

        private async Task<Orden> FindFirstOrden(int? id)
        {
            var pedido = await _context.Orden.Include(o => o.IdStatusOrdenNavigation).
                 Include(o => o.IdDomicilioNavigation).Include(o => o.IdUsuarioNavigation).
                 FirstOrDefaultAsync(o => o.IdOrden == id);
            return pedido;
        }


    }
}
