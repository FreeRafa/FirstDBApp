using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using FirstDBApp.Modelos.Entidades;


namespace FirstDBApp.Infraestrutura.Data
{
    public class FirstDBAppContext : DbContext
    {
       
        public DbSet<Cliente> Clientes { get; set; }

        public FirstDBAppContext(DbContextOptions<FirstDBAppContext> options) : base(options)
        {
        }

        public FirstDBAppContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("FirstDBApp");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(c => c.Nome)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(c => c.Email)
                      .HasMaxLength(200)
                      .IsRequired();
            });
        }
    }
}
