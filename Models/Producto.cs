using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Carrito = new HashSet<Carrito>();
            Compra = new HashSet<Compra>();
            DetalleOrden = new HashSet<DetalleOrden>();
            Ofertas = new HashSet<Ofertas>();
        }

        [Key]
        public int IdProducto { get; set; }
        [StringLength(70)]
        public string Nombre { get; set; }
        [Column("descripcion", TypeName = "text")]
        public string Descripcion { get; set; }
        public double? Precio { get; set; }
        [Column(TypeName = "text")]
        public string Foto { get; set; }
        public int IdCategoria { get; set; }
        public int? Stock { get; set; }

        [ForeignKey(nameof(IdCategoria))]
        [InverseProperty(nameof(Categoria.Producto))]
        public virtual Categoria IdCategoriaNavigation { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<Carrito> Carrito { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<Compra> Compra { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
        [InverseProperty("IdProductoNavigation")]
        public virtual ICollection<Ofertas> Ofertas { get; set; }
    }
}
