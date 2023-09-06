using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.Ports.Out;

namespace ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductsInteractor : ISearchProductsInteractor
    {
        private readonly IProductRepository _productRepository;

        public SearchProductsInteractor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<SearchProductsPortOut> ExecuteAsync(SearchProductsPortIn portIn)
        {
            var products = await _productRepository.SearchAsync(portIn.Key);

            var searchProductPortOut = new SearchProductsPortOut(products.ToList());
            
            return searchProductPortOut;
        }
    }
}
