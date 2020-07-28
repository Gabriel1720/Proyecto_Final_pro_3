using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Producto = new HashSet<Producto>();
        }

        [Key]
        public int IdCategoria { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }

        [InverseProperty("IdCategoriaNavigation")]
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
