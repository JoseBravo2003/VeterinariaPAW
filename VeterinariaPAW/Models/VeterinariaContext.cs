using Microsoft.EntityFrameworkCore;
using VeterinariaPAW.Models;

namespace VeterinariaPAW.Models
{
    public class VeterinariaContext : DbContext
    {
        public VeterinariaContext(DbContextOptions<VeterinariaContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Producto> Producto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar el mapeo de las claves foráneas para que coincidan con los nombres de la base de datos
            modelBuilder.Entity<Producto>()
                .Property(p => p.IdCategoria)
                .HasColumnName("IdCategoria");

            modelBuilder.Entity<Producto>()
                .Property(p => p.IdProveedor)
                .HasColumnName("IdProveedor");

            // Configurar las relaciones explícitamente si es necesario
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedor)
                .WithMany(pr => pr.Productos)
                .HasForeignKey(p => p.IdProveedor);

            base.OnModelCreating(modelBuilder);
        }
    }
}
