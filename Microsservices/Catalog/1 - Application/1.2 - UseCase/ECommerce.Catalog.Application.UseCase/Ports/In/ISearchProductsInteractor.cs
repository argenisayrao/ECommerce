using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;

namespace ECommerce.Catalog.Application.UseCase.Ports.In
{
    public interface ISearchProductsInteractor
    {
        Task<IReadOnlyCollection<SearchProductsPortOut>> ExecuteAsync(SearchProductsPortIn portIn);
    }
}
