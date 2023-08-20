using ECommerce.Application.DomainModel.Entities;
using ECommerce.Application.DomainModel.Notifiables;
using ECommerce.Application.UseCase.Ports.In;
using ECommerce.Application.UseCase.Ports.Out;

namespace ECommerce.Application.UseCase.UseCase.AddProduct
{
    public class AddProductInteractor: AbstractNotifiable, IAddProductInteractor
    {
        private readonly IProductRepository _productRepository;

        public AddProductInteractor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<AddProductPortOut> ExecuteAsync(AddProductPortIn dataPortIn)
        {
            var product = new Product(Guid.NewGuid(), dataPortIn.Name, dataPortIn.Value);
            AddNotifications(product);

            if(Invalid)
            {
                return new AddProductPortOut(false, "Invalid product",
                    Notifications, Guid.Empty, "", 0);   
            }

            await _productRepository.AddAsync(product);

            return new AddProductPortOut(true, "Valid product",
                Notifications,product.Id, product.Name, product.Value);
        }
    }
}

