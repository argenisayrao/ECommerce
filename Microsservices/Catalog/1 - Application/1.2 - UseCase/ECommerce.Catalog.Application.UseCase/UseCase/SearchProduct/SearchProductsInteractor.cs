using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.Util;

namespace ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductsInteractor : ISearchProductsInteractor
    {
        private readonly IProductRepository _productRepository;

        public SearchProductsInteractor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PageListDto<SearchProductPortOut>> ExecuteAsync(SearchProductsPortIn portIn)
        {
            var products = await _productRepository.SearchAsyncByName(new SearchProductFilter(portIn));

            var response =  new PageListDto<SearchProductPortOut>(products, products.Items.Select(x=> new SearchProductPortOut(x)).ToList());

            return response;
        }
    }
}
