using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    public class GestionPedidosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
