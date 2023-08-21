namespace ECommerce.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductsPortOut
    {
        public SearchProductsPortOut(Guid id, string name, double value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }

    }
}
