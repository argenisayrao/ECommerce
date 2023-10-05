namespace ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductsPortIn
    {
        public string Name { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

