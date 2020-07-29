using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class Ofertas
    {
        [Key]
        public int IdOfertas { get; set; }
        public double Precio { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FechaInicio { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FechaFin { get; set; }
        public int? IdProducto { get; set; }

        [ForeignKey(nameof(IdProducto))]
        [InverseProperty(nameof(Producto.Ofertas))]
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
