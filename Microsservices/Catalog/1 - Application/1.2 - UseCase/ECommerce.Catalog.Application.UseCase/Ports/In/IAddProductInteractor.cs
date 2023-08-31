using ECommerce.Catalog.Application.UseCase.UseCase.AddProduct;

namespace ECommerce.Catalog.Application.UseCase.Ports.In
{
    public interface IAddProductInteractor
    {
        Task<AddProductPortOut> ExecuteAsync(AddProductPortIn dataPortIn);
    }
}
