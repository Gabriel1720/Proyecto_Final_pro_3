using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CambiarSlider : Controller
    {
        DB_A64A4C_SuperMercadoContext _context = new DB_A64A4C_SuperMercadoContext();
        public async Task<IActionResult> Index()
        {
             ViewBag.Foto = await _context.Slider.ToListAsync();           
            return View();
        }

        public async Task<IActionResult> Cambiar(List<IFormFile> foto1 = null, List<IFormFile> foto2 = null, List<IFormFile> foto3 = null)
        {
            
            //Actiualizacion de la foto 1
            if(foto1.Count != 0)
            { 
                Slider slider = new Slider();   
                foreach (var item in foto1)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            slider.Foto = stream.ToArray();
                            slider.IdSlider = 1;
                        }
                    }
                }
                _context.Update(slider);
            }

            //Actiualizacion de la foto 2

            if (foto2.Count != 0)
            {
                Slider slider = new Slider();
                foreach (var item in foto2)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            slider.Foto = stream.ToArray();
                            slider.IdSlider = 2;
                        }
                    }
                }
                _context.Update(slider);
            }

            //Actiualizacion de la foto 3

            if (foto3.Count != 0)
            {
                Slider slider = new Slider();
                foreach (var item in foto3)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            slider.Foto = stream.ToArray();
                            slider.IdSlider = 3;
                        }
                    }
                }
                _context.Update(slider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
