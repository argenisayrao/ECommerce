using ECommerce.Application.UseCase.UseCase.GetProductById;

namespace ECommerce.Application.UseCase.Ports.In
{
    public interface IGetProductByIdInteractor
    {
        Task<GetProductByIdPortOut> ExecuteAsync(GetProductByIdPortIn dataPortIn);
    }
}
