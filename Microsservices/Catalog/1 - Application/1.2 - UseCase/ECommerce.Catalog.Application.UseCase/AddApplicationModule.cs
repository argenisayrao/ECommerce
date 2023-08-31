using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.UseCase.AddProduct;
using ECommerce.Catalog.Application.UseCase.UseCase.GetProductById;
using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Catalog.Application.UseCase
{
    public static class AddApplicationModule
    {
        public static IServiceCollection AddProductApplication<TProductRepository>(this IServiceCollection services)
            where TProductRepository : class, IProductRepository
        {
            services.AddScoped<IProductRepository, TProductRepository>();
            return services;
        }

        public static IServiceCollection AddProductUseCaseApplication(this IServiceCollection services)
        {
            services.AddScoped<IAddProductInteractor, AddProductInteractor>();
            services.AddScoped<ISearchProductsInteractor, SearchProductsInteractor>();
            services.AddScoped<IGetProductByIdInteractor, GetProductByIdInteractor>();

            return services;
        }
    }
}
