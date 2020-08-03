using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class MiCuentaCliente : Controller
    {
        public DB_A64A4C_SuperMercadoContext CT = new DB_A64A4C_SuperMercadoContext();
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
