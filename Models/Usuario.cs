using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

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
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public int IdRol { get; set; }
        public string Password { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<Compra> Compra { get; set; }
        public virtual ICollection<Domicilio> Domicilio { get; set; }
        public virtual ICollection<Orden> Orden { get; set; }
    }
}
