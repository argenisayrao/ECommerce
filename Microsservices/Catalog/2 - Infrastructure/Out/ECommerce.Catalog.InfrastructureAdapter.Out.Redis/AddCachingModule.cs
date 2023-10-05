using ECommerce.Catalog.Application.UseCase.Ports.Out;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Catalog.InfrastructureAdapter.Out.Redis
{
    public static class AddCachingModule
    {
        public static IServiceCollection AddCachingScope(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICachingRepository, CachingRepository>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = ConstantsRedis.InstaceName;
                options.Configuration = configuration.GetConnectionString($"{ConstantsRedis.Address},{ConstantsRedis.Password}");
            });

            return services;
        }
    }
}
