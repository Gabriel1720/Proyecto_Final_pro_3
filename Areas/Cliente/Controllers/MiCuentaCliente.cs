using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_final_pro_3.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class MiCuentaCliente : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ordenes()
        {

            return View();
        }
    }
}
