using System ; 
using Microsoft.AspNetCore.Mvc;

namespace Tienda_.Controllers
{
    public class CuentaController : Controller
    {
      
    public IActionResult Login(){

       return View(); 
    }

    public IActionResult Nueva_cuenta() {
        return View(); 
    }

    public IActionResult Registrase() {
        return View();
    }

         
    }
}