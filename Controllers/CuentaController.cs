using System ; 
using Microsoft.AspNetCore.Mvc;

namespace Tienda_.Controllers
{
    public class CuentaController : Controller
    {
      
        public IActionResult Login()
        {
            return View(); 
        }

        public IActionResult Nueva_cuenta() 
        {
            return View(); 
        }

        public IActionResult Registrarse() 
        {
            return View();
        }
    
        public IActionResult Detalle_Producto()
        {
                return View();
        }

        public IActionResult Cuenta_Administrador()
        {
            return View();
        }  
    }
}