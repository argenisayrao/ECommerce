using Ex.Arq.Hex.Application.DomainModel;
using Ex.Arq.Hex.Application.DomainModel.Entities;
using Ex.Arq.Hex.Application.UseCase.Ports.Out;
using Ex.Arq.Hex.InfrastructureAdapter.Out.AccessData.EntityFramework.Products.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Ex.Arq.Hex.Unit.Integration.Config
{
    public class ConfigProductIntegrationTest
    {
        public ConfigProductIntegrationTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IProductRepository, ProductRepository>();
            serviceCollection.AddSingleton<Product>(new Product(Guid.NewGuid(), "Mesa", 2.99));

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider ServiceProvider { get; private set; }
    }
}
