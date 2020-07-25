using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GestionReportes : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DetalleCompra()
        {
            return View();
        }
    }
}
