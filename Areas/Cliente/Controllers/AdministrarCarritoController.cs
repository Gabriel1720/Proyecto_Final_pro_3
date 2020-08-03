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
    public class AdministrarCarritoController : Controller
    {
        DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();

        public async Task<IActionResult> Index()
        {
            string idUser = HttpContext.Session.GetString("userID");
            int id = Int32.Parse(idUser);
            var carrito = await _context.Carrito.Include(x => x.IdProductoNavigation).Where(x => x.IdUsuario == id).ToListAsync();         

            var ofertas = await _context.Ofertas.ToListAsync();
            double? TotalDescuento = 0;
            double? SubTotal = 0;
            double? TotalPagar = 0;
            foreach (var itemCarrito in carrito)
            {
                SubTotal = SubTotal + itemCarrito.IdProductoNavigation.Precio * itemCarrito.Cantidad;
                foreach (var itemOferta in ofertas)
                {
                    if (itemCarrito.IdProducto == itemOferta.IdProducto)
                    {
                        if (itemOferta.Precio >= 0)
                        {
                            TotalDescuento = TotalDescuento + itemOferta.Precio * itemCarrito.Cantidad;
                        }
                    }
                }
            }
            TotalPagar = SubTotal - TotalDescuento;

            ViewBag.Descuento = TotalDescuento;
            ViewBag.SubTotal = SubTotal;
            ViewBag.Total = TotalPagar;

            string session = HttpContext.Session.GetString("userID");
            ViewBag.UserID = session;
            return View(carrito);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var carrito = await _context.Carrito.FindAsync(id);
            _context.Remove(carrito);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> addCarrito(Carrito cart)
        {
            string idUser = HttpContext.Session.GetString("userID");


            if (idUser != null)
            {

                int id = Int32.Parse(idUser);
                var existe = _context.Carrito.Where(x => x.IdUsuario == id && x.IdProducto == cart.IdProducto).Count();

                if (existe == 1)
                {
                    Carrito carrito = await _context.Carrito.FirstOrDefaultAsync(x => x.IdUsuario == id && x.IdProducto == cart.IdProducto);
                    carrito.Cantidad += cart.Cantidad;
                    cart.IdUsuario = Convert.ToInt32(idUser);

                    _context.Update(carrito);
                }
                else
                {
                    cart.IdUsuario = Convert.ToInt32(idUser);

                    // add el los datos a la db 
                    await _context.Carrito.AddAsync(cart);
                }

                // guardar los cambios realizados a la db 
                int guardado = await _context.SaveChangesAsync();

                if (guardado > 0)
                {
                    TempData["added"] = "success";
                    return RedirectToAction("Detalle_Producto", "Home", new { id = cart.IdProducto });                   
                }
                TempData["added"] = "error";
                return RedirectToAction("Detalle_Producto", "Home", new { id = cart.IdProducto });
            }

            return RedirectToAction("Login", "Cuenta");

        }

    }
}
