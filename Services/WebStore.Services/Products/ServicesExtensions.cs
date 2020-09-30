using Microsoft.Extensions.DependencyInjection;
using WebStore.Interfaces.Services;
using WebStore.Services.Products.InCookies;
using WebStore.Services.Products.InSQL;

namespace WebStore.Services.Products
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddWebStoreServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeesData, SQLEmployeeData>();
            services.AddScoped<IProductData, SQLProductData>();
            services.AddScoped<IOrderService, SQLOrderService>();
            services.AddScoped<ICartService, CookiesCartService>();

            return services;
        }
    }
}
