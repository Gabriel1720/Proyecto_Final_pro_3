using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        public DB_A64A4C_SuperMercadoContext _contex = new DB_A64A4C_SuperMercadoContext();

        public async Task<IActionResult> Index()
        {
            ViewBag.Productos = await _contex.Producto.Where(x => x.Precio >= 0 || x.Precio <= 100).ToListAsync();

            // ViewBag.UserID = Request.Cookies["userID"];
            ViewBag.UserID = HttpContext.Session.GetString("userID"); 
            return View();
        }

        public async Task<IActionResult> Detalle_Producto(int? id)
        {         
            ViewBag.producto = await _contex.Producto.FindAsync(id);
            ViewBag.Productos = await _contex.Producto.Where(x => x.Precio >= 0 || x.Precio <= 100).ToListAsync();
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Categorias(int? id) {
            var listaProductos = await _contex.Producto.Where(producto => producto.IdCategoria == id).ToListAsync();
            ViewBag.Productos = listaProductos;     
            return View();     
        }



        public IActionResult Top_menu_bar()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
