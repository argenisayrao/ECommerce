using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;
using ECommerce.Catalog.Application.UseCase.Util;

namespace ECommerce.Catalog.Application.UseCase.Ports.In
{
    public interface ISearchProductsInteractor
    {
        Task<PageListDto<SearchProductPortOut>> ExecuteAsync(SearchProductsPortIn portIn);
    }
}
