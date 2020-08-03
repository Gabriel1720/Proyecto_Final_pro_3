using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class Compra
    {
        [Key]
        public int IdCompra { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }
        public int? Cantidad { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int? IdOrden { get; set; }
 

        [ForeignKey(nameof(IdOrden))]
        [InverseProperty(nameof(Orden.Compra))]
        public virtual Orden IdOrdenNavigation { get; set; }
        [ForeignKey(nameof(IdProducto))]
        [InverseProperty(nameof(Producto.Compra))]
        public virtual Producto IdProductoNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Compra))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
       
    }
}
