using System ;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Proyecto_final_pro_3.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;


namespace Tienda_.Controllers
{
    public class CuentaController : Controller
    {
        private readonly static DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();
 

        [HttpGet]
        public IActionResult Login() {
            return View(); 
        
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login(UserLogin userLogin)
        {
            if (ModelState.IsValid) {
                Usuario userDB = _context.Usuario.Where(user => user.Correo == userLogin.Correo && user.Password == userLogin.Password).FirstOrDefault();
                
                if (userDB != null)
                {
                   // establecer la session 
                  HttpContext.Session.SetString("userID", userDB.IdUsuario.ToString());

                    // opciones de los cookies 
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(5); 

                    // establecer los cookies 
                    Response.Cookies.Append("UserID", userDB.IdUsuario.ToString(), options);


                    if (userDB.IdRol == 1)
                    {
                        return RedirectToAction("Cuenta", "PerfilAdmin", new { area = "Admin" });
                    }

                    return RedirectToAction("Index", "Home");

                }

                ViewBag.mensaje = "El usuario no existe";  
            }


            return View(userLogin);      

        }

     
        public IActionResult Nueva_cuenta() 
        {
            return View(); 
        }

        public IActionResult Registrarse() 
        {
            return View();
        }


        

        public IActionResult LoggedOut()
        {
            // Response.Cookies.Delete("useID");
            HttpContext.Session.Clear(); 

            return RedirectToAction("Login"); 
        }


    }
}