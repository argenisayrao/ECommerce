using ECommerce.Catalog.Application.UseCase.Ports.Out;
using Microsoft.Extensions.Caching.Distributed;

namespace ECommerce.Catalog.InfrastructureAdapter.Out.Redis
{
    public class CachingRepository : ICachingRepository
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;
        public CachingRepository(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };
        }

        public async Task<string> GetAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await _cache.SetStringAsync(key, value);
        }
    }
}
