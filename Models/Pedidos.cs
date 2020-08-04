using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_final_pro_3.Models
{
    public class Pedidos
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string latlong { get; set; }
        public string Estado { get; set; }
        public string Monto { get; set; }
    }
}
