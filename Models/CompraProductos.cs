using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_final_pro_3.Models
{
    public class CompraProductos
    {
        public int IdUser {get; set;}
        public int cantidad { get; set; }
        public int IdProducto { get; set; } 
        public double total { get; set; }
        public double precio { get; set; }
        public float latitud { get; set;}
        public float longitud { get; set; }
        public string comentario { get; set; }
        public string telefono { get; set; }
    }
}
