using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.Services.Interfaces;
using Newtonsoft.Json;

namespace ECommerce.Catalog.Application.UseCase.UseCase.GetProductById
{
    public class GetProductByIdInteractor : IGetProductByIdInteractor
    {
        private readonly IProductRepository _productRepository;
        private readonly ICachingService _cache; 

        public GetProductByIdInteractor(IProductRepository productRepository, ICachingService cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        public async Task<GetProductByIdPortOut> ExecuteAsync(GetProductByIdPortIn dataPortIn)
        {
            var productCache = await _cache.GetAsync(dataPortIn.Id.ToString());

            if (!string.IsNullOrWhiteSpace(productCache))
            {
                var productFromCache = JsonConvert.DeserializeObject<Product>(productCache);

                if (productFromCache is not null)
                    return new GetProductByIdPortOut(productFromCache.Id.ToString(),
                        productFromCache.Name, productFromCache.Value);
            }

            var product = await _productRepository.GetByIdAsync(dataPortIn.Id);
            if (product is not null)
            {
                await _cache.SetAsync(dataPortIn.Id.ToString(), JsonConvert.SerializeObject(product));
                return new GetProductByIdPortOut(product.Id.ToString(), product.Name, product.Value);
            }                

            return new GetProductByIdPortOut(false);
        }
    }
}
