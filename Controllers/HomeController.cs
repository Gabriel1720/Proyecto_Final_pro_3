using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proyecto_final_pro_3.Models; 

namespace Tienda_.Controllers
{
    public class HomeController : Controller
    {
        public static DB_A64A4C_SuperMercadoContext _contex = new DB_A64A4C_SuperMercadoContext();

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

            if (session != null ) {
                   
             
             }
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




        public async Task<IActionResult> addCarrito(Carrito cart)
        {
            string session = HttpContext.Session.GetString("userID");
            if (session != null )
            {
                cart.IdUsuario = int.Parse(session);

                // add el los datos a la db 
                await _contex.Carrito.AddAsync(cart);

                // guardar los cambios realizados a la db 
                int guardado = await _contex.SaveChangesAsync();

                if (guardado > 0)
                {
                    return RedirectToAction("Detalle_Producto", "Home", new { id = cart.IdProducto });
                }
            }

            return RedirectToAction("Login", "Cuenta");
        }

 
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

  
}
