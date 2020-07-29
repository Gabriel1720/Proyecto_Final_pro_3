using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Carrito = new HashSet<Carrito>();
            Compra = new HashSet<Compra>();
            Domicilio = new HashSet<Domicilio>();
            Orden = new HashSet<Orden>();
            SliderUser = new HashSet<SliderUser>();
        }

        [Key]
        public int IdUsuario { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Apellido { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FechaNacimiento { get; set; }
        [StringLength(100)]
        public string Correo { get; set; }
        public int IdRol { get; set; }
        [Column("_Password")]
        [StringLength(50)]
        public string Password { get; set; }

        [ForeignKey(nameof(IdRol))]
        [InverseProperty(nameof(Rol.Usuario))]
        public virtual Rol IdRolNavigation { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Carrito> Carrito { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Compra> Compra { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Domicilio> Domicilio { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Orden> Orden { get; set; }
        [InverseProperty("IdUserNavigation")]
        public virtual ICollection<SliderUser> SliderUser { get; set; }
    }
}
