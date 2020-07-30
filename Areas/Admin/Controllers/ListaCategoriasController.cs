using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ListaCategoriasController : Controller
    {

        public DB_A64A4C_SuperMercadoContext CT = new DB_A64A4C_SuperMercadoContext();

        public ListaCategoriasController()
        {



        }
        public IActionResult Index()
        {
            ViewBag.Categoria = CT.Categoria.ToList();
            return View();
        }



        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {

            if (!ModelState.IsValid)
            {
                return View(categoria);

            }

            CT.Categoria.Add(categoria);
            CT.SaveChanges();


            return RedirectToAction("Index");
        }

    }
}
