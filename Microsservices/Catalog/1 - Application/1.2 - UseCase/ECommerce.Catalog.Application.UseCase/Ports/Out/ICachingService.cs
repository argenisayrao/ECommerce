namespace ECommerce.Catalog.Application.UseCase.Ports.Out
{
    public interface ICachingService
    {
        Task SetAsync(string key, string value);
        Task<string> GetAsync(string key);
    }
}
