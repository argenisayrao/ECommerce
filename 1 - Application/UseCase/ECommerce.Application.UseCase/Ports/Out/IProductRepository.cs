using ECommerce.Application.DomainModel.Entities;

namespace ECommerce.Application.UseCase.Ports.Out
{
    public interface IProductRepository
    {
        public Task<Product> GetByIdAsync(Guid id);
        public Task<IReadOnlyCollection<Product>> SearchAsync(string key);
        public Task AddAsync(Product product);
    }
}
