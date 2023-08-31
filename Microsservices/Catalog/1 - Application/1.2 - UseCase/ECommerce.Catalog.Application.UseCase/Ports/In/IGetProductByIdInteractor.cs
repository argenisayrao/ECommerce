using ECommerce.Catalog.Application.UseCase.UseCase.GetProductById;

namespace ECommerce.Catalog.Application.UseCase.Ports.In
{
    public interface IGetProductByIdInteractor
    {
        Task<GetProductByIdPortOut> ExecuteAsync(GetProductByIdPortIn dataPortIn);
    }
}
