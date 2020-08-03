using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Ordenes()
        {
            string idUser = HttpContext.Session.GetString("userID");
            int id = Int32.Parse(idUser);

            //var usuario = await CT.Orden.Where(x => x.IdUsuario == id).ToListAsync();

            List<Pedidos> lst;
            using (DB_A64A4C_SuperMercadoContext CT = new DB_A64A4C_SuperMercadoContext()) 
            {
                lst = (from o in CT.Orden
                       join e in CT.StatusOrden on o.IdStatusOrden equals e.IdStatusOrden
                      join d in CT.Domicilio on o.IdDomicilio equals d.IdDomicilio
                      where o.IdUsuario == id
                      select new Pedidos
                      {
                          Id = o.IdOrden,
                          Fecha = o.Fecha.ToString(),
                          latlong = d.Latitud.ToString() + " " + d.Longitud.ToString(),
                          Estado = e.Nombre,
                          Monto = o.Total,
                      }).ToList();
            }

            return View(lst);
        }
    }
}
