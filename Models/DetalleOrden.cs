using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class DetalleOrden
    {
        [Key]
        public int IdDetalleOrden { get; set; }
        public double? Precio { get; set; }
        public double? Cantidad { get; set; }
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }

        [ForeignKey(nameof(IdOrden))]
        [InverseProperty(nameof(Orden.DetalleOrden))]
        public virtual Orden IdOrdenNavigation { get; set; }
        [ForeignKey(nameof(IdProducto))]
        [InverseProperty(nameof(Producto.DetalleOrden))]
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
