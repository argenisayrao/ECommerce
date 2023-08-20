using Microsoft.Extensions.DependencyInjection;
using ECommerce.Application.UseCase;
using ECommerce.InfrastructureAdapter.Out.AccessData.EntityFramework.Contexts;
using ECommerce.InfrastructureAdapter.Out.AccessData.EntityFramework.Products.Repository;

namespace ECommerce.InfrastructureAdapter.Out.AccessData
{
    public static class AddAccessDataModule
    { 
        public static IServiceCollection AddApplicationWithAccessData(this IServiceCollection services)
        {
            services.AddDbContext<AppDb>();
            services.AddProductApplication<ProductRepository>();
            services.AddProductUseCaseApplication();

            return services;
        }
    }
}
