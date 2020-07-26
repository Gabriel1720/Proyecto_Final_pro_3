using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Utilidades : Controller
    {
        DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();
        int indice = 0;
        public async Task<IActionResult> Index()
        {
            ViewBag.Usuario = await _context.Usuario.ToListAsync();
            return View();
        }

        public async Task<IActionResult> IndexMes (int mes)
        {
            if(mes == 0)
            {
                ViewBag.error = "Debe elegir una opcion";
                ViewBag.Usuario = await _context.Usuario.ToListAsync();
                return View("Index");
            }
            indice = mes;
            ViewBag.Usuario = await _context.Usuario.Where(x => x.FechaNacimiento.Value.Month == mes).ToListAsync();

            return View("Index");
        }

        public async Task<IActionResult> ReadCsv()
        {
            List<Usuario> User = null;
            if (indice == 0)
            {
                User = await _context.Usuario.ToListAsync();
            }
            else
            {
                User = await _context.Usuario.Where(x => x.FechaNacimiento.Value.Month == indice).ToListAsync();
            }
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Nombre,Apellido,Fecha Nacimiento,Email");
            
            foreach(Usuario usuario in User)
            {
                csv.AppendLine($"{usuario.Nombre},{usuario.Apellido},{usuario.FechaNacimiento},{usuario.Correo}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv","usuariosInfo.csv");
        }
    }
}
