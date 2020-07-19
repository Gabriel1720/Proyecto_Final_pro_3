using System;
using System.Collections.Generic;

namespace Proyecto_final_pro_3.Models
{
    public partial class Domicilio
    {
        public Domicilio()
        {
            Orden = new HashSet<Orden>();
        }

        public int IdDomicilio { get; set; }
        public double? Longitud { get; set; }
        public double? Latitud { get; set; }
        public string Direccion { get; set; }
        public string Comentario { get; set; }
        public int IdUsuario { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Orden> Orden { get; set; }
    }
}
