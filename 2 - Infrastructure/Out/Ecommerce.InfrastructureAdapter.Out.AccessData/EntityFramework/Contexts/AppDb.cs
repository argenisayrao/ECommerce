using FluentValidator;
using Microsoft.EntityFrameworkCore;
using ECommerce.Application.DomainModel.Entities;
using ECommerce.InfrastructureAdapter.Out.AccessData.EntityFramework.Products;

namespace ECommerce.InfrastructureAdapter.Out.AccessData.EntityFramework.Contexts
{
    public class AppDb:DbContext
    {
        public AppDb()
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductFA());
            modelBuilder.Ignore<Notification>();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-UHI8IM2M\SQLEXPRESS;Database=ECommerce;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
