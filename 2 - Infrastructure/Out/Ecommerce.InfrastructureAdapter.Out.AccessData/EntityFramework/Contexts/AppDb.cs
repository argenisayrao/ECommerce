using FluentValidator;
using Microsoft.EntityFrameworkCore;
using ECommerce.Application.DomainModel.Entities;
using ECommerce.InfrastructureAdapter.Out.AccessData.EntityFramework.Products;
using Microsoft.Extensions.Configuration;
using Ecommerce.InfrastructureAdapter.Out.AccessData.Helpers;

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
            optionsBuilder.UseSqlServer("connection");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
