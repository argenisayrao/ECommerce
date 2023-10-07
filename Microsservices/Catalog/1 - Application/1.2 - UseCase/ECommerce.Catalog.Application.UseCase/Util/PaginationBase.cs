namespace ECommerce.Catalog.Application.UseCase.Util
{
    public class PaginationBase
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
    }
}
