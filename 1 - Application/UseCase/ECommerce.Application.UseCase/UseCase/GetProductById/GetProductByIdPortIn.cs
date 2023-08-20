namespace ECommerce.Application.UseCase.UseCase.GetProductById
{
    public class GetProductByIdPortIn
    {
        public GetProductByIdPortIn(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
