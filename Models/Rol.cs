using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        public int IdRol { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }

        [InverseProperty("IdRolNavigation")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
