using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class Orden
    {
        public Orden()
        {
            Compra = new HashSet<Compra>();
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        [Key]
        public int IdOrden { get; set; }
        [StringLength(20)]
        public string Total { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }
        public int IdDomicilio { get; set; }
        public int IdUsuario { get; set; }
        public int? IdStatusOrden { get; set; }

        [ForeignKey(nameof(IdDomicilio))]
        [InverseProperty(nameof(Domicilio.Orden))]
        public virtual Domicilio IdDomicilioNavigation { get; set; }
        [ForeignKey(nameof(IdStatusOrden))]
        [InverseProperty(nameof(StatusOrden.Orden))]
        public virtual StatusOrden IdStatusOrdenNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Orden))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
        [InverseProperty("IdOrdenNavigation")]
        public virtual ICollection<Compra> Compra { get; set; }
        [InverseProperty("IdOrdenNavigation")]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
