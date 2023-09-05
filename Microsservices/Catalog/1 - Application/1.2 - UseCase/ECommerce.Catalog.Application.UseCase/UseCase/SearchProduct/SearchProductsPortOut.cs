using MongoDB.Bson;

namespace ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductsPortOut
    {
        public SearchProductsPortOut(string id, string name, double value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }

    }
}
