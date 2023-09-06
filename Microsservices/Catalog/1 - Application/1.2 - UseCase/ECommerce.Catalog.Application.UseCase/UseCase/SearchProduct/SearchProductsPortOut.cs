using ECommerce.Catalog.Application.DomainModel.Entities;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductsPortOut
    {
        public SearchProductsPortOut(List<Product> products)
        {
            products.ForEach(product =>
            {
                SearchProductPortOut.Add(
                new SearchProductPortOut(product.Id.ToString(),
                                         product.Name,
                                         product.Value));
            });
        }
        public List<SearchProductPortOut> SearchProductPortOut { get; private set; } = new();
    }

    public class SearchProductPortOut
    {
        public SearchProductPortOut(string id, string name, double value)
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
