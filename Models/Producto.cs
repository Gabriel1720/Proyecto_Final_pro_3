using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_final_pro_3.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Carrito = new HashSet<Carrito>();
            Compra = new HashSet<Compra>();
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        public int IdProducto { get; set; }
        [Required(ErrorMessage ="No debe dejar este campo vacio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "No debe dejar este campo vacio")]

        public string Descripcion { get; set; }
        [Required(ErrorMessage = "No debe dejar este campo vacio")]

        public double? Precio { get; set; }
        [Required(ErrorMessage = "No debe dejar este campo vacio")]
        public string Foto { get; set; }

        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }
        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<Compra> Compra { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
