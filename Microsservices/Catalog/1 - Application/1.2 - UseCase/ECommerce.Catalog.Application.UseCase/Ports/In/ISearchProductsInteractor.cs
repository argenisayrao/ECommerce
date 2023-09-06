using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;

namespace ECommerce.Catalog.Application.UseCase.Ports.In
{
    public interface ISearchProductsInteractor
    {
        Task<SearchProductsPortOut> ExecuteAsync(SearchProductsPortIn portIn);
    }
}
