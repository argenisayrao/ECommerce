using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.Services.Interfaces;

namespace ECommerce.Catalog.Application.UseCase.Services
{
    internal class CachingService : ICachingService
    {
        private readonly ICachingRepository _cache;

        public CachingService(ICachingRepository cache)
        {
            _cache = cache;
        }

        public async Task<string> GetAsync(string key)
        {
            return await _cache.GetAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await _cache.SetAsync(key, value);
        }
    }
}
