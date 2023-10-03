using ECommerce.Catalog.Application.DomainModel.Entities;

namespace ECommerce.Catalog.Application.UseCase.Ports.Out
{
    public interface IProductRepository
    {
        public Task<Product> GetByIdAsync(Guid id);
        public Task<IReadOnlyCollection<Product>> SearchAsyncByName(string key);
        public Task AddAsync(Product product);
    }
}
