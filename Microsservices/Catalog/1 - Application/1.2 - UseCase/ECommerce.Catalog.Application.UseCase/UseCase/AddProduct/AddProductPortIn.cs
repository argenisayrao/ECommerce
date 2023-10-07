namespace ECommerce.Catalog.Application.UseCase.UseCase.AddProduct
{
    public class AddProductPortIn
    {
        public AddProductPortIn(string id, string name, double value)
        {
            Id = Guid.Parse(id);
            Name = name;
            Value = value;
        }
        public Guid Id {get;private set;}
        public string Name { get; private set; }
        public double Value { get; private set; }
    }
}
