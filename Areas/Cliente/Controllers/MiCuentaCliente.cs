using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class MiCuentaCliente : Controller
    {
        DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();

        public async Task<IActionResult> Index()
        {
            string sesion = HttpContext.Session.GetString("userID");
            int useID = Convert.ToInt32(sesion);

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == useID);

            ViewBag.UserID = sesion;

            if (sesion == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }

            return View(usuario);
        }

        public async Task<IActionResult> Edit(string nombre, string apellido, int idUser)
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == idUser);
            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CambiarContraseña()
        {
            string sesion = HttpContext.Session.GetString("userID");
            int useID = Convert.ToInt32(sesion);

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == useID);

            ViewBag.UserID = sesion;

            if (sesion == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }

            return View(usuario);
        }

        public async Task<IActionResult> EditPassword(int id, string contrasena_actual, string nueva_contrasena, string confirmar_contrasena)
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);

            if (usuario.Password == contrasena_actual) 
            {
                if (!string.IsNullOrEmpty(nueva_contrasena) && !string.IsNullOrEmpty(nueva_contrasena) &&  nueva_contrasena == confirmar_contrasena)
                {
                    usuario.Password = nueva_contrasena;
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error2 = "Las contraseñas no coinciden";
                    return View("CambiarContraseña", usuario);
                }
            }
            else
            {
                ViewBag.Error1 = "Contraseña invalida";
                return View("CambiarContraseña", usuario);
            }                   
        }

        public async Task<IActionResult> Ordenes()
        {
            string idUser = HttpContext.Session.GetString("userID");
            if (idUser == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }

            int id = Int32.Parse(idUser);       
            //int id = 2;
            //var usuario = await CT.Orden.Where(x => x.IdUsuario == id).ToListAsync();
            List<Pedidos> lst;
            using (DB_A64A4C_SuperMercadoContext CT = new DB_A64A4C_SuperMercadoContext()) 
            {
                lst = await (from o in CT.Orden
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
                      }).ToListAsync();
            }

            ViewBag.userID = id; 

            return View(lst);
        }


        [HttpGet]
        public async Task <IActionResult> DetailsOrden(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var orden = await _context.Orden.Include(o => o.IdDomicilioNavigation).
                Include(o => o.IdStatusOrdenNavigation).FirstOrDefaultAsync(o=> o.IdOrden==id);

            var detalleOrden = await _context.DetalleOrden.Where(d => d.IdOrden == id).
               Include(d => d.IdProductoNavigation).
               ToListAsync();

            var ofertas = await _context.Ofertas.ToListAsync();

            double? totalDescuento = 0;
            foreach (var itemDetalle in detalleOrden)
            {
                foreach (var itemOfertas in ofertas)
                {
                    if (itemDetalle.IdProducto == itemOfertas.IdProducto)
                    {
                        if (itemOfertas.Precio >= 0)
                        {
                            totalDescuento = totalDescuento + itemOfertas.Precio * itemDetalle.Cantidad;
                        }
                    }
                }
            }
            ViewData["TotalDescuento"] = totalDescuento;
            ViewData["detalleOrden"] = detalleOrden;
            return View(orden);
        }
    }
}
