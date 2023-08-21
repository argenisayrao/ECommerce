using ECommerce.Application.UseCase.UseCase.SearchProduct;

namespace ECommerce.Application.UseCase.Ports.In
{
    public interface ISearchProductsInteractor
    {
        Task<IReadOnlyCollection<SearchProductsPortOut>> ExecuteAsync(SearchProductsPortIn portIn);
    }
}
