using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_final_pro_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PerfilAdminController : Controller
    {
        public DB_A64A4C_SuperMercadoContext _context;

        public PerfilAdminController(DB_A64A4C_SuperMercadoContext context) {
            _context = context; 
        }

        public IActionResult Index()
        {          
           return View();
        }

        public async Task<IActionResult> Cuenta()
        {
            int loggedUSerID = Convert.ToInt32(HttpContext.Session.GetString("userID")); 

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == loggedUSerID);


            if (usuario != null) {
                return View(usuario);
            }
            return RedirectToAction("Login", "Cuenta"); 
        }

        public async Task<IActionResult> Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                
                return View("Cuenta");
            }

            return RedirectToAction("Cuenta");            
        }

        public IActionResult CambiarContrasena()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPassword(string contrasena_actual, string nueva_contrasena, string confirmar_contrasena)
        {
            if(String.IsNullOrEmpty(contrasena_actual) || String.IsNullOrEmpty(nueva_contrasena) || String.IsNullOrEmpty(confirmar_contrasena))
            {
                ViewBag.error_campos = "Contraseña invalida";
                return View("CambiarContrasena");
            }

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Correo == "Oscarmora20@gmail.com" && x.Password == contrasena_actual);
            if(usuario == null)
            {
                ViewBag.error_usuario = "Contraseña invalida";
                return View("CambiarContrasena");
            }

            if (nueva_contrasena == confirmar_contrasena)
            {
                usuario.Password = nueva_contrasena;
                _context.Update(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Contrasena_Cambiada");
            }
            else
            {
                ViewBag.error_contrasena = "Las contraseña no coinciden";
                return View("CambiarContrasena");
            }           
        }

        public IActionResult Contrasena_Cambiada ()
        {
            return View();
        }
        public IActionResult Nuevo_Producto()
        {
            return View();
        }
        public IActionResult Detalle_Producto_Admin()
        {
            return View();
        }
    }
}
