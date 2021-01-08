using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Proyecto_final_pro_3.Models;

namespace Proyecto_final_pro_3.Models
{
    public partial class DB_A64A4C_SuperMercadoContext : DbContext
    {
         

        public DB_A64A4C_SuperMercadoContext(DbContextOptions<DB_A64A4C_SuperMercadoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<DetalleOrden> DetalleOrden { get; set; }
        public virtual DbSet<Domicilio> Domicilio { get; set; }
        public virtual DbSet<Ofertas> Ofertas { get; set; }
        public virtual DbSet<Orden> Orden { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<SliderUser> SliderUser { get; set; }
        public virtual DbSet<StatusOrden> StatusOrden { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
       
    }
}
