using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_final_pro_3.Models;
using ReflectionIT.Mvc.Paging;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Utilidades : Controller
    {
        DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();
      
        public async Task<IActionResult> Index(int page = 1, int mes = 0)
        {            
            if(mes != 0)
            {
                Guardar.Mes = mes;
            }         
        
            var query = _context.Usuario.AsNoTracking().Where(x => x.FechaNacimiento.Value.Month == Guardar.Mes).OrderBy(x => x.IdUsuario);

            var model = await PagingList.CreateAsync(query, 6, page);
            ViewBag.Usuario = model;            
            return View(model);      
        
        }


        public async Task<IActionResult> ReadCsv(int indice)
        {
            List<Usuario> user = null;
            if (indice == 0)
            {
                user = await _context.Usuario.ToListAsync();
            }
            else
            {
                user = await _context.Usuario.Where(x => x.FechaNacimiento.Value.Month == indice).ToListAsync();
            }
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Nombre,Apellido,Fecha Nacimiento,Email");
            
            foreach(Usuario usuario in user)
            {
                csv.AppendLine($"{usuario.Nombre},{usuario.Apellido},{usuario.FechaNacimiento},{usuario.Correo}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv","usuariosInfoNuevos.csv"); 

        }

        public async Task<IActionResult> Details(int? id)
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);
            return View(usuario);
        }
    }
}
