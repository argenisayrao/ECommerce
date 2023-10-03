using ECommerce.Catalog.Application.UseCase.Ports.Out;

namespace ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB.Caching
{
    public class CachingService : ICachingService
    {
        public Task<string> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
