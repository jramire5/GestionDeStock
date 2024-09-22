using GestionDeStock.Model;

using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using System.Reflection.Metadata;

namespace GestionDeStock
{
    internal class GestionContext : DbContext
    {
        String connectionString = "server=localhost;port=3306;user=root;password=root;database=gestiondestock;";//Para hacerlo en MySQL
        internal DbSet<Producto> Productos { get; set; }
        internal DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasKey(p => p.id);
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
        }
        internal GestionContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionString);// para MySQL
            //optionsBuilder.UseSqlServer(@"Server=(localhost)\MSSQLLocalDB;Initial Catalog=gestiondestock");//Para SQL Server
        }
    }
}
