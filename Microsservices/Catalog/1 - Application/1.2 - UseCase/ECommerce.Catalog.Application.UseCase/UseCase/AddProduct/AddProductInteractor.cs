using ECommerce.Catalog.Application.DomainModel.Entities;
using ECommerce.Catalog.Application.DomainModel.Notifiables;
using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.Ports.Out;

namespace ECommerce.Catalog.Application.UseCase.UseCase.AddProduct
{
    public class AddProductInteractor : AbstractNotifiable, IAddProductInteractor
    {
        private readonly IProductRepository _productRepository;

        public AddProductInteractor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<AddProductPortOut> ExecuteAsync(AddProductPortIn dataPortIn)
        {
            var product = new Product(dataPortIn.Id, dataPortIn.Name, dataPortIn.Value);
            AddNotifications(product);

            if (Invalid)
            {
                return new AddProductPortOut(false, "Invalid product",
                    Notifications,"", "", 0);
            }

            await _productRepository.AddAsync(product);

            return new AddProductPortOut(true, "Valid product",
                Notifications, product.Id.ToString(), product.Name, product.Value);
        }
    }
}

