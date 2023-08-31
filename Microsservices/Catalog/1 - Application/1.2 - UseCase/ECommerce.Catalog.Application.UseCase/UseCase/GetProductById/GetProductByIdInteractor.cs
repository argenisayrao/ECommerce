using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.Ports.Out;

namespace ECommerce.Catalog.Application.UseCase.UseCase.GetProductById
{
    public class GetProductByIdInteractor : IGetProductByIdInteractor
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdInteractor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductByIdPortOut> ExecuteAsync(GetProductByIdPortIn dataPortIn)
        {
            var product = await _productRepository.GetByIdAsync(dataPortIn.Id);
            if (product is not null)
                return new GetProductByIdPortOut(product.Id, product.Name, product.Value);

            return new GetProductByIdPortOut(false);
        }
    }
}
