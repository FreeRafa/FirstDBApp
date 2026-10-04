using FirstDBApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using FirstDBApp.Data;


namespace FirstDBApp.Data
{
    public class FirstDBAppContext : DbContext
    {
       
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfig.GetConnectionString());
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
