using MongoDB.Bson;

namespace ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductsPortOut
    {
        public SearchProductsPortOut(ObjectId id, string name, double value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }

    }
}
