namespace ECommerce.Catalog.Application.UseCase.Util
{
    public sealed class PageListDto<Type> : PaginationBase
    {
        public PageListDto()
        {
        }

        public PageListDto(PaginationBase result, IList<Type> items)
        {
            Items = items;
            Page = result.Page;
            PageSize = result.PageSize;
            Total = result.Total;
            TotalPages = result.TotalPages;
        }

        public IList<Type> Items { get; set; } = new List<Type>();
    }
}
