using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PruebaCarritoController : Controller
    {
        readonly DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();
        public async Task< IActionResult> Index()
        {
            var carrito = await _context.Carrito.Where(c => c.IdUsuario == 47).
                Include(c=> c.IdProductoNavigation).ToListAsync();
            var ofertas = _context.Ofertas;        
            double TotalDescuento = 0;
            double? SubTotal = 0;
            double? TotalPagar = 0;
               foreach(var itemCarrito in carrito)
               {
                SubTotal = SubTotal + itemCarrito.IdProductoNavigation.Precio * itemCarrito.Cantidad;
                foreach (var itemOferta in ofertas)
                    {                         
                        if(itemCarrito.IdProducto == itemOferta.IdProducto)
                        {
                            if (itemOferta.Precio >= 0)
                            {                            
                                TotalDescuento = TotalDescuento + itemOferta.Precio;                               
                            }
                        }
                    }            
               }
            TotalPagar = SubTotal - TotalDescuento;
            return View();
        }
    }
}
