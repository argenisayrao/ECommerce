using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.Services.Interfaces;
using ECommerce.Catalog.Application.UseCase.UseCase.GetProductById;
using ECommerce.Catalog.Application.UseCase.Util;
using Newtonsoft.Json;

namespace ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductsInteractor : ISearchProductsInteractor
    {
        private readonly IProductRepository _productRepository;
        private readonly ICachingService _cache;

        public SearchProductsInteractor(IProductRepository productRepository, ICachingService cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        public async Task<PageListDto<SearchProductPortOut>> ExecuteAsync(SearchProductsPortIn portIn)
        {
            var productsCache = await _cache.GetAsync($"{portIn.Name.ToLower()}{portIn.Page}{portIn.PageSize}");

            if (!string.IsNullOrWhiteSpace(productsCache))
            {
                var productsFromCache = JsonConvert.DeserializeObject<PageListDto<Product>>(productsCache);

                if (productsFromCache is not null)
                    return new PageListDto<SearchProductPortOut>(productsFromCache,
                        productsFromCache.Items.Select(x => new SearchProductPortOut(x)).ToList());
            }

            var products = await _productRepository.SearchAsyncByName(new SearchProductFilter(portIn));

            var response =  new PageListDto<SearchProductPortOut>(products, products.Items.Select(x=> new SearchProductPortOut(x)).ToList());
            await _cache.SetAsync($"{portIn.Name.ToLower()}{portIn.Page}{portIn.PageSize}", JsonConvert.SerializeObject(response));

            return response;
        }
    }
}
