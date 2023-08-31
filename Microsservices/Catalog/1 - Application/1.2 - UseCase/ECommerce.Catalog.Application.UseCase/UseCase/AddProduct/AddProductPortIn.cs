namespace ECommerce.Catalog.Application.UseCase.UseCase.AddProduct
{
    public class AddProductPortIn
    {
        public AddProductPortIn(string name, double value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; private set; }
        public double Value { get; private set; }
    }
}
