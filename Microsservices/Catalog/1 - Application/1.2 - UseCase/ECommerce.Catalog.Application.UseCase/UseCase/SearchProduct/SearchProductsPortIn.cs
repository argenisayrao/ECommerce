namespace ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct
{
    public class SearchProductsPortIn
    {
        public SearchProductsPortIn(string key)
        {
            Key = key;
        }
        public string Key { get; private set; }
    }
}

