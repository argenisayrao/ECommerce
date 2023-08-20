using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECommerce.Application.DomainModel.Entities;

namespace ECommerce.InfrastructureAdapter.Out.AccessData.EntityFramework.Products
{
    internal class ProductFA : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.Property(product => product.Name).HasMaxLength(100).IsRequired();
        }
    }
}
