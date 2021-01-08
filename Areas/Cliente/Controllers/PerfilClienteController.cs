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
    public class PerfilClienteController : Controller
    {
        private readonly DB_A64A4C_SuperMercadoContext _context; 

        public PerfilClienteController(DB_A64A4C_SuperMercadoContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            string sesion = HttpContext.Session.GetString("userID");
            ViewBag.UserID = sesion;

            if(sesion == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }

            return View();
        }

        public async Task<IActionResult> CambiarContraseña(string contrasena_actual, string nueva_contrasena, string confirmar_contrasena)
        {
            string sesion = HttpContext.Session.GetString("userID");
            int useID = Convert.ToInt32(sesion);

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == useID);



            ViewBag.UserID = sesion;


            if (sesion == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }

            return View(usuario);
        }
    }
}
