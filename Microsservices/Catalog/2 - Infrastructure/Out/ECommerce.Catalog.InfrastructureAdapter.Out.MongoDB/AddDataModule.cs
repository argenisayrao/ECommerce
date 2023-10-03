using ECommerce.Catalog.Application.UseCase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB.Repository;
using ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB.Constants;
using ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB.Caching;

namespace ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB
{
    public static class AddDataModule
    {
        public static IServiceCollection AddAplicationWithAccessData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoCollection(configuration);
            services.AddProductApplication<ProductRepository>();
            services.AddCachingServiceApplication<CachingService>(configuration.GetConnectionString(ConstantsRedis.Address));
            services.AddProductUseCaseApplication();

            return services;
        }

        public static IServiceCollection AddMongoCollection(this IServiceCollection services, IConfiguration configuration)
        {
            var collection = new MongoClient(configuration.GetConnectionString(ConstantsMongo.MongoDBConnection))
                .GetDatabase(ConstantsMongo.MongoDataBaseName);

            services.AddSingleton(collection);

            return services;
        }
    }
}
