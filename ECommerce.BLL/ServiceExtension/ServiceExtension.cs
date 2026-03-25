using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.BLL
{
    public static class ServiceExtension
    {
        public static void AddBLLService(this IServiceCollection services)
        {
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IRoleManagerService, RoleManagerService>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IImageManager, ImageManager>();
            services.AddValidatorsFromAssembly(typeof(ServiceExtension).Assembly);
            services.AddScoped<ICategoryMapper, CategoryMapper>();
            services.AddScoped<IProductMapper, ProductMapper>();
            services.AddScoped<IErrorMapper, ErrorMapper>();
        }
    }
}
