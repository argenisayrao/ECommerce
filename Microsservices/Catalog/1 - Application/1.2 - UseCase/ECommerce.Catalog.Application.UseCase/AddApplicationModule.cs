using ECommerce.Catalog.Application.UseCase.Ports.In;
using ECommerce.Catalog.Application.UseCase.Ports.Out;
using ECommerce.Catalog.Application.UseCase.Services;
using ECommerce.Catalog.Application.UseCase.Services.Interfaces;
using ECommerce.Catalog.Application.UseCase.UseCase.AddProduct;
using ECommerce.Catalog.Application.UseCase.UseCase.GetProductById;
using ECommerce.Catalog.Application.UseCase.UseCase.SearchProduct;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Catalog.Application.UseCase
{
    public static class AddApplicationModule
    {
        public static IServiceCollection AddProductApplicationForEvents<TProductRepository>(this IServiceCollection services)
            where TProductRepository : class, IProductRepository
        {
            services.AddScoped<IProductRepository, TProductRepository>();
            services.AddScoped<IAddProductInteractor, AddProductInteractor>();
            return services;
        }

        public static IServiceCollection AddProductApplicationForEventsWebApi<TProductRepository>(this IServiceCollection services)
            where TProductRepository : class, IProductRepository
        {
            services.AddScoped<IProductRepository, TProductRepository>();
            services.AddScoped<ICachingService, CachingService>();
            services.AddScoped<ISearchProductsInteractor, SearchProductsInteractor>();
            services.AddScoped<IGetProductByIdInteractor, GetProductByIdInteractor>();

            return services;
        }
    }
}
