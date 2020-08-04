using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_final_pro_3.Models
{
    public class CompraProductos
    {
        [Key]
        public int id { get; set; }
        public int IdUser {get; set;}
        public int? cantidad { get; set; }
        public int IdProducto { get; set; } 
        public double total { get; set; }
        public double precio { get; set; }
        public double latitud { get; set;}
        public double longitud { get; set; }
        public string comentario { get; set; }
        public string telefono { get; set; }
    }
}
