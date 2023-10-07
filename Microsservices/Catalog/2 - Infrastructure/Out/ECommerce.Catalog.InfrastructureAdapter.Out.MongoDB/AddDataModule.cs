using ECommerce.Catalog.Application.UseCase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ECommerce.Catalog.InfrastructureAdapter.Out.MongoDB
{
    public static class AddDataModule
    {
        public static IServiceCollection AddAplicationWithAccessDataForEvents(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoCollection(configuration);
            services.AddProductApplicationForEvents<ProductRepository>();

            return services;
        }

        public static IServiceCollection AddAplicationWithAccessDataForWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoCollection(configuration);
            services.AddProductApplicationForEventsWebApi<ProductRepository>();

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
