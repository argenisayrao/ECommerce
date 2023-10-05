using ECommerce.Catalog.Application.DomainModel.Entities;

namespace ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductPortOut
    {
        public SearchProductPortOut(Product product)
        {
            Id = product.Id.ToString(); ;
            Name = product.Name;
            Value = product.Value;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }
    }
}
