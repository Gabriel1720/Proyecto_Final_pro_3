using System;
using System.Collections.Generic;

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
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double? Precio { get; set; }
        public string Foto { get; set; }
        public int IdCategoria { get; set; }
        public int? Stock { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<Compra> Compra { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
