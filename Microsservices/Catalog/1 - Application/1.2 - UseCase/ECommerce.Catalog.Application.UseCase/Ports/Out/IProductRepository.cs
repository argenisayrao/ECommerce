using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;
using ECommerce.Catalog.Application.UseCase.Util;

namespace ECommerce.Catalog.Application.UseCase.Ports.Out
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<PageListDto<Product>> SearchAsyncByName(SearchProductFilter filter);
        Task AddAsync(Product product);
    }
}
