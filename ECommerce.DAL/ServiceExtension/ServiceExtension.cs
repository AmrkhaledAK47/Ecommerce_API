using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ECommerce.DAL
{
    public static class ServiceExtension
    {
        public static void AddDALService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppContext>(
                (options) =>
                {
                    options
                    .UseSqlServer(connectionString)
                    .UseAsyncSeeding(async (context, _, _) =>
                    {
                        if (await context.Set<Product>().AnyAsync() || await context.Set<Category>().AnyAsync())
                        {
                            return;
                        }
                        if (await context.Set<AppUser>().AnyAsync() || await context.Set<AppRole>().AnyAsync() || await context.Set<IdentityUserRole<string>>().AnyAsync())
                        {
                            return;
                        }
                        var products = SeedDataProvider.GetProducts();
                        var categories = SeedDataProvider.GetCategories();
                        var roles = SeedDataProvider.GetRoles();
                        var users = SeedDataProvider.GetUsers();
                        var userRoles = SeedDataProvider.GetUserRoles();

                        await context.Set<Product>().AddRangeAsync(products);
                        await context.Set<Category>().AddRangeAsync(categories);
                        await context.Set<AppRole>().AddRangeAsync(roles);
                        await context.Set<AppUser>().AddRangeAsync(users);
                        await context.Set<IdentityUserRole<string>>().AddRangeAsync(userRoles);
                        await context.SaveChangesAsync();
                    })
                    .UseSeeding((context, _) =>
                    {
                        if (context.Set<Product>().Any() || context.Set<Category>().Any())
                        {
                            return;
                        }
                        if (context.Set<AppUser>().Any() || context.Set<AppRole>().Any() || context.Set<IdentityUserRole<string>>().Any())
                        {
                            return;
                        }
                        var products = SeedDataProvider.GetProducts();
                        var categories = SeedDataProvider.GetCategories();
                        var roles = SeedDataProvider.GetRoles();
                        var users = SeedDataProvider.GetUsers();
                        var userRoles = SeedDataProvider.GetUserRoles();

                        context.Set<Product>().AddRange(products);
                        context.Set<Category>().AddRange(categories);
                        context.Set<AppRole>().AddRange(roles);
                        context.Set<AppUser>().AddRange(users);
                        context.Set<IdentityUserRole<string>>().AddRange(userRoles);
                        context.SaveChanges();
                    });

                });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
