using System ;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 
using Microsoft.EntityFrameworkCore;
using Proyecto_final_pro_3.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web; 

namespace Tienda_.Controllers
{
    public class CuentaController : Controller
    {
        private readonly static DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();


        public IActionResult Login() {
            return View(); 
        
        }

         

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login(Usuario user)
        {
           if (ModelState.IsValid) {
               
                Usuario fromDBuser = _context.Usuario.Where(db_user => db_user.Correo == user.Correo).FirstOrDefault();

                if (isValid(fromDBuser)) {
                    // crear cookies 
                   //  HttpCookie cookies = new HttpCookie(); 

                    

                    if (fromDBuser.IdRol == 2) {
                        return RedirectToAction("Cuenta", "PerfilAdmin", new { area = "Admin" });
                     
                    }
                        return RedirectToAction("Index", "Home");
                }
           

        }
    
          return RedirectToAction("Cuenta", "PerfilAdmin", new { area = "Admin" });
   
        }

        public IActionResult Nueva_cuenta() 
        {
            return View(); 
        }

        public IActionResult Registrarse() 
        {
            return View();
        }


        // verificar que el usuario existe en la base de datos 

        public   static  bool isValid(Usuario usuario) {

            Usuario fromDBuser =  _context.Usuario.Where(db_user => db_user.Correo == usuario.Correo).FirstOrDefault(); 

            if (fromDBuser != null) {
                if (fromDBuser.Correo == usuario.Correo && fromDBuser.Password == usuario.Password)
                {
                    return true;
                }
            }

            return false; 
 
        }

 
    }
}