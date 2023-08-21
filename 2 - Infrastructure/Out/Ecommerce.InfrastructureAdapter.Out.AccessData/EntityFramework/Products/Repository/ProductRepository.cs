using Microsoft.EntityFrameworkCore;
using ECommerce.Application.DomainModel.Entities;
using ECommerce.Application.UseCase.Ports.Out;
using ECommerce.InfrastructureAdapter.Out.AccessData.EntityFramework.Contexts;

namespace ECommerce.InfrastructureAdapter.Out.AccessData.EntityFramework.Products.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDb _db;

        public ProductRepository(AppDb db)
        {
            _db = db;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(productDb => productDb.Id == id);
            return product;
        }

        public async Task<IReadOnlyCollection<Product>> SearchAsync(string key = "")
        {
            key = key.ToLower();
            var productsQuery = _db.Products.AsQueryable();
            if (key.Any())
                productsQuery = _db.Products.Where(productDb => productDb.Name.ToLower().Contains(key));

            var products = await productsQuery.ToListAsync();
            return products;
        }
        public async Task AddAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
        }
    }

}

