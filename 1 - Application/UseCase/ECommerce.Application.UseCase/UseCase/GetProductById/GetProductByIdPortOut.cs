namespace ECommerce.Application.UseCase.UseCase.GetProductById
{
    public class GetProductByIdPortOut
    {
        public GetProductByIdPortOut(Guid id, string name, double value)
        {
            Id = id;
            Name = name;
            Value = value;
            IsExists = true;
        }

        public GetProductByIdPortOut(bool isExists)
        {
            IsExists = isExists;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }
        public bool IsExists { get; private set; }
    }
}
