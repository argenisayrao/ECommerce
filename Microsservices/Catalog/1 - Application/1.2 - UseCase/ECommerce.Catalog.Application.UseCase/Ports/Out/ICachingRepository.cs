namespace ECommerce.Catalog.Application.UseCase.Ports.Out
{
    public interface ICachingRepository
    {
        Task SetAsync(string key, string value);
        Task<string> GetAsync(string key);
    }
}
