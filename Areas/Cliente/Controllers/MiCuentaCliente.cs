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
    public class MiCuentaCliente : Controller
    {
        DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();

        public async Task<IActionResult> Index()
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

        public async Task<IActionResult> Edit(string nombre, string apellido, int idUser)
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == idUser);
            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CambiarContraseña()
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

        public async Task<IActionResult> EditPassword(int id, string contrasena_actual, string nueva_contrasena, string confirmar_contrasena)
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);

            if (usuario.Password == contrasena_actual) 
            {
                if (!string.IsNullOrEmpty(nueva_contrasena) && !string.IsNullOrEmpty(nueva_contrasena) &&  nueva_contrasena == confirmar_contrasena)
                {
                    usuario.Password = nueva_contrasena;
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error2 = "Las contraseñas no coinciden";
                    return View("CambiarContraseña", usuario);
                }
            }
            else
            {
                ViewBag.Error1 = "Contraseña invalida";
                return View("CambiarContraseña", usuario);
            }                   
        }
    }
}
