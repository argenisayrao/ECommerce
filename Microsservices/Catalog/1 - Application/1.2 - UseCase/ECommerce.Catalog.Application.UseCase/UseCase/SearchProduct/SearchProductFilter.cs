using Ecommerce.Core.Domain;

namespace ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductFilter : FilterPage
    {
        public SearchProductFilter(SearchProductsPortIn searchProductsPortIn)
        {
            Name = searchProductsPortIn.Name;
            Page = searchProductsPortIn.Page;
            PageSize = searchProductsPortIn.PageSize;
        }

        public string Name { get; set; }
    }
}
