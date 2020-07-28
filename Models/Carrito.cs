using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class Carrito
    {
        [Key]
        public int IdCarrito { get; set; }
        public int? Cantidad { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }

        [ForeignKey(nameof(IdProducto))]
        [InverseProperty(nameof(Producto.Carrito))]
        public virtual Producto IdProductoNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Carrito))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
