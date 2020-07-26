using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AgregarUsuarios : Controller
    {
        public readonly DB_A64A4C_SuperMercadoContext CT = new DB_A64A4C_SuperMercadoContext();

        public AgregarUsuarios()
        {


        }
        public IActionResult Index()
        {
            ViewBag.Usuario = CT.Usuario.ToList();
            return View();
        }
    }
}
