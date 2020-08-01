using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                model = PagingList.Create(pedidos, 7, page);               
            }
            else
            {
                Guardar.PedidoBucar = "";
                pedidos = await GetAllPedidos();
                model = PagingList.Create(pedidos, 7, page);
            }
            if (pedidos == null)
            {
                return NotFound();
            }
            
            return View(model);
        }


        private async Task<List<Orden> >SearhPedido(string PedidoBuscar)
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

        private async Task<List<Orden>>GetAllPedidos()
        {
            List<Orden> pedidos;
            pedidos = await _context.Orden.Include(o => o.IdStatusOrdenNavigation).
                   Include(o => o.IdDomicilioNavigation).Include(o => o.IdUsuarioNavigation).
                   OrderByDescending(o => o.IdOrden).ToListAsync();
            return pedidos;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPedidosMap()
        {
            List<Orden> pedidos = await GetAllPedidos();
           
            if(pedidos == null)
            {
                return NotFound();
            }
            return View(pedidos);
        }
    }
}
