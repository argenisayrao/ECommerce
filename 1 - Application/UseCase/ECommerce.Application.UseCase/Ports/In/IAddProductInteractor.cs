using ECommerce.Application.UseCase.UseCase.AddProduct;

namespace ECommerce.Application.UseCase.Ports.In
{
    public interface IAddProductInteractor
    {
        Task<AddProductPortOut> ExecuteAsync(AddProductPortIn dataPortIn);
    }
}
