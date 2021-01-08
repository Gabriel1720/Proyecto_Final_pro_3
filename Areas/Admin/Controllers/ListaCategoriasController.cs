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

        public DB_A64A4C_SuperMercadoContext _context;

        public ListaCategoriasController(DB_A64A4C_SuperMercadoContext context) {
            _context = context; 
        }

        public IActionResult Index()
        {
            ViewBag.Categoria = _context.Categoria.ToList();
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

            _context.Categoria.Add(categoria);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

    }
}
