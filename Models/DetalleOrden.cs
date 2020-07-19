using System;
using System.Collections.Generic;

namespace Proyecto_final_pro_3.Models
{
    public partial class DetalleOrden
    {
        public int IdDetalleOrden { get; set; }
        public double? Precio { get; set; }
        public double? Cantidad { get; set; }
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }

        public virtual Orden IdOrdenNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
