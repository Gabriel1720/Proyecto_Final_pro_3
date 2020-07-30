using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_final_pro_3.Models;
using ReflectionIT.Mvc.Paging;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuariosAdminController : Controller
    {
        public DB_A64A4C_SuperMercadoContext CT = new DB_A64A4C_SuperMercadoContext();

        public UsuariosAdminController()
        {

        }
        public async Task<IActionResult> Index(string srt = "", int page = 1)
        {
            IOrderedQueryable<Usuario> usuarios = null;
            if (!String.IsNullOrEmpty(srt))
            {
                Guardar.UsuarioSrt = srt;
                usuarios = CT.Usuario.AsNoTracking().Where(x => x.IdRol == 1)
                           .Where(x => x.Nombre == Guardar.UsuarioSrt || x.Correo == Guardar.UsuarioSrt).OrderBy(x => x.IdUsuario);                
            }
            else if(String.IsNullOrEmpty(srt) || String.IsNullOrEmpty(Guardar.UsuarioSrt))
            {
                usuarios = CT.Usuario.AsNoTracking().Where(x => x.IdRol == 1).OrderBy(x => x.IdUsuario);
                Guardar.UsuarioSrt = null;
            }
            
            ViewBag.UsuarioSrt = Guardar.UsuarioSrt;           
            var model = await PagingList.CreateAsync(usuarios, 7, page);
            return View(model);
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