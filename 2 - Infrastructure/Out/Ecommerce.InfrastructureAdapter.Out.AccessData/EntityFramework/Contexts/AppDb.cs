using FluentValidator;
using Microsoft.EntityFrameworkCore;
using ECommerce.Application.DomainModel.Entities;
using ECommerce.InfrastructureAdapter.Out.AccessData.EntityFramework.Products;
using Microsoft.Extensions.Configuration;

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
            optionsBuilder.UseSqlServer(@);
            base.OnConfiguring(optionsBuilder);
        }

        public static IConfigurationRoot GetAppSettings()
        {
            return new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(TryGetSolutionDirectoryInfo().FullName,
                     @"Vale.ECOS.Noise.Integration.Api"))
                 .AddJsonFile("appsettings.Development.json")
                 .Build();
        }

        public static DirectoryInfo TryGetSolutionDirectoryInfo(string? currentPath = null)
        {
            var directory = new DirectoryInfo(
            currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
    }
}
