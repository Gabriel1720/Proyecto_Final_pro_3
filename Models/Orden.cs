using System;
using System.Collections.Generic;

namespace Proyecto_final_pro_3.Models
{
    public partial class Orden
    {
        public Orden()
        {
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        public int IdOrden { get; set; }
        public string Status { get; set; }
        public string Total { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdDomicilio { get; set; }
        public int IdUsuario { get; set; }

        public virtual Domicilio IdDomicilioNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
