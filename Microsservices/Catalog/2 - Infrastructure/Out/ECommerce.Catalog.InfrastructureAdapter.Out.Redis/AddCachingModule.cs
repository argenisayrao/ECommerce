using ECommerce.Catalog.Application.UseCase.Ports.Out;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Catalog.InfrastructureAdapter.Out.Redis
{
    public static class AddCachingModule
    {
        public static IServiceCollection AddCachingScope(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = $"{configuration.GetConnectionString(ConstantsRedis.Address)}," +
                $"{configuration.GetConnectionString(ConstantsRedis.Password)}";

            services.AddScoped<ICachingRepository, CachingRepository>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = ConstantsRedis.InstaceName;
                options.Configuration = connection;
            });

            return services;
        }
    }
}
