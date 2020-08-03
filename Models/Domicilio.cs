using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class Domicilio
    {
        public Domicilio()
        {
            Orden = new HashSet<Orden>();
        }

        [Key]
        public int IdDomicilio { get; set; }
        public double? Longitud { get; set; }
        public double? Latitud { get; set; }
        [Column(TypeName = "text")]
        public string Comentario { get; set; }
        public int IdUsuario { get; set; }
        [StringLength(15)]
        public string Telefono { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Domicilio))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
        [InverseProperty("IdDomicilioNavigation")]
        public virtual ICollection<Orden> Orden { get; set; }
    }
}
