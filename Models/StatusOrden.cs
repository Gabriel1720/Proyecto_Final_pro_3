using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class StatusOrden
    {
        public StatusOrden()
        {
            Orden = new HashSet<Orden>();
        }

        [Key]
        public int IdStatusOrden { get; set; }
        [StringLength(20)]
        public string Nombre { get; set; }

        [InverseProperty("IdStatusOrdenNavigation")]
        public virtual ICollection<Orden> Orden { get; set; }
    }
}
