using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuariosAdminController : Controller
    {
        public readonly DB_A64A4C_SuperMercadoContext CT = new DB_A64A4C_SuperMercadoContext();

        public UsuariosAdminController()
        {


        }
        public IActionResult Index()
        {
            ViewBag.Usuario = CT.Usuario.Where(x => x.IdRol == 1).ToList();
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {

            if (!ModelState.IsValid)
            {
                return View(usuario);

            }

            usuario.IdRol = 1;

            CT.Usuario.Add(usuario);
            int A = CT.SaveChanges();
            if (A > 0)
            {
                return RedirectToAction("Index");


            }

            return RedirectToAction("Create");

        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await CT.Usuario
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }



    }
}