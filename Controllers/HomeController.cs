using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proyecto_final_pro_3.Models; 

namespace Tienda_.Controllers
{
    public class HomeController : Controller
    {
        public DB_A64A4C_SuperMercadoContext _contex = new DB_A64A4C_SuperMercadoContext();

        public async Task<IActionResult> Index()
        {
       
            ViewBag.Foto = await _contex.Slider.ToListAsync();

            ViewBag.Productos = await _contex.Producto.Where(x => x.Precio >= 0 || x.Precio <= 100).ToListAsync();
          //  string cookies = Request.Cookies["userID"];


         //   if (cookies != null) {
                // verificar la session a partir de los cookies 
            //    HttpContext.Session.SetString("userID", cookies);

           // }

            // ViewBag.UserID = Request.Cookies["userID"];
            string session = HttpContext.Session.GetString("userID");
            ViewBag.UserID = session; 
 
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detalle_Producto(int? id)
        {
            var productoDB = await _contex.Producto.Where(x => x.IdProducto == id).FirstOrDefaultAsync();
            ViewBag.producto = productoDB;
            ViewBag.Productos = await _contex.Producto.Where(x => x.IdCategoria == productoDB.IdCategoria).ToArrayAsync();

            string session = HttpContext.Session.GetString("userID");
            ViewBag.UserID = session;

            if (TempData.ContainsKey("added"))
            {
                ViewBag.Added = TempData["added"].ToString();
            }
 
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Categorias(int? id) {
            var listaProductos = await _contex.Producto.Where(producto => producto.IdCategoria == id).ToListAsync();
            var categoria = await _contex.Categoria.Where(x => x.IdCategoria == listaProductos.FirstOrDefault().IdCategoria).FirstOrDefaultAsync();

            if (categoria != null) {
                ViewBag.Productos = listaProductos;
                ViewBag.categoria = categoria.Nombre;
                ViewBag.cantidad = listaProductos.Count();
            }

            string session = HttpContext.Session.GetString("userID");
            ViewBag.UserID = session;

            return View();
        }

        public IActionResult Conctactanos()
        {           
            string session = HttpContext.Session.GetString("userID");
            ViewBag.UserID = session;
            return View();
        }



     
        public async Task<IActionResult> Busqueda_Producto(string producto)
        {

            var listaProductos = await _contex.Producto.Where(x => x.Nombre.Contains(producto)).ToListAsync();

            ViewBag.Productos = listaProductos;
            ViewBag.categoria = "Resultados de la busqueda";
            ViewBag.cantidad = listaProductos.Count();

            string session = HttpContext.Session.GetString("userID");
            ViewBag.UserID = session;

            return View();

        }

        public async Task<IActionResult> Ofertas()
        {
            var listaProductos = await _contex.Ofertas.Include(x => x.IdProductoNavigation)
                                .Where(x => x.FechaInicio.Value.Date <= DateTime.Now.Date && x.FechaFin >= DateTime.Now.Date).ToListAsync();

            ViewBag.Productos = listaProductos;
            ViewBag.categoria = "Productos en oferta";
            ViewBag.cantidad = listaProductos.Count();

            string session = HttpContext.Session.GetString("userID");
            ViewBag.UserID = session;
            return View();
        }

        public async Task<IActionResult> Comprado(string telefono, string comentario, string lat, string lon, string  id, string cantidad) {
            
            // get the logged user id 
            int userID = int.Parse(HttpContext.Session.GetString("userID"));

            // si el id del producto se encuentra entonces comprar ese producto 
            if (id != null)
            {
                var p = await _contex.Producto.Where(x => x.IdProducto == int.Parse(id)).FirstOrDefaultAsync();   
                await _contex.Database.ExecuteSqlRawAsync($"comprarProducto {userID}, {int.Parse(cantidad)}, {p.IdProducto}, {p.Precio}, {p.Precio}, {float.Parse(lat)}, {float.Parse(lon)}, '{comentario}', '{telefono}'");
              
                return RedirectToAction("Detalle_Producto", "Home", new { id = p.IdProducto });
            }

            // si el id del carrito es numo entonces debe realizar este proceso
            // comprar lo procutos del carrito del cliente 

            var carrito = await _contex.Carrito.Include(x => x.IdProductoNavigation).Where(x => x.IdUsuario == userID).ToListAsync();

                var ofertas = await _contex.Ofertas.ToListAsync();
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


                foreach (var p in carrito)
                { 
       
                    
                    await _contex.Database.ExecuteSqlRawAsync($"comprarCarrito {userID}, {p.Cantidad}, {p.IdProducto}, {TotalPagar}, {p.IdProductoNavigation.Precio}, {float.Parse(lat)}, {float.Parse(lon)}, '{comentario}', '{telefono}'");

                }

                return RedirectToAction("index", "AdministrarCarrito", new { area = "Cliente" });

            }





          
 
    


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

  
}
