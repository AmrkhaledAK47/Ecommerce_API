using Microsoft.AspNetCore.Identity;

namespace ECommerce.DAL
{
    public static class SeedDataProvider
    {
        public static List<Product> GetProducts()
        {
            var DateTimeNow = new DateTime(2026, 3, 4, 12, 0, 0);
            return new List<Product>
            {
                new Product {  Title = "Laptop", Description = "High performance laptop",CreatedAt = DateTimeNow, Price = 1200.00m, Count = 10, CategoryId = 2 },
                new Product {  Title = "Smartphone", Description = "Latest model smartphone",CreatedAt = DateTimeNow, Price = 800.00m, Count = 25, CategoryId = 1 },
                new Product {  Title = "Headphones", Description = "Noise cancelling headphones",CreatedAt = DateTimeNow, Price = 150.00m, Count = 40, CategoryId = 3 },
                new Product {  Title = "Monitor", Description = "4K UHD monitor",CreatedAt = DateTimeNow, Price = 350.00m, Count = 15, CategoryId = 2 },
                new Product {  Title = "Keyboard", Description = "Mechanical keyboard",CreatedAt = DateTimeNow, Price = 90.00m, Count = 30, CategoryId = 3 },
                new Product {  Title = "Mouse", Description = "Wireless mouse",CreatedAt = DateTimeNow, Price = 45.00m, Count = 50, CategoryId = 3 },
                new Product {  Title = "Tablet", Description = "10-inch tablet",CreatedAt = DateTimeNow, Price = 400.00m, Count = 20, CategoryId = 1 },
                new Product {  Title = "Webcam", Description = "HD webcam",CreatedAt = DateTimeNow, Price = 70.00m, Count = 18, CategoryId = 3 },
                new Product {  Title = "Printer", Description = "Laser printer",CreatedAt = DateTimeNow, Price = 200.00m, Count = 12, CategoryId = 2 },
                new Product {  Title = "Charger", Description = "Fast charger",CreatedAt = DateTimeNow, Price = 30.00m, Count = 60, CategoryId = 3 }
            };
        }
        public static List<Category> GetCategories()
        {
            var DateTimeNow = new DateTime(2026, 3, 4, 12, 0, 0);
            return new List<Category>
            {
                new Category { CreatedAt = DateTimeNow, Name = "Electronics" },
                new Category { CreatedAt = DateTimeNow, Name = "Computers" },
                new Category { CreatedAt = DateTimeNow, Name = "Accessories" }
            };
        }
        public static List<AppRole> GetRoles()
        {
            var adminRoleId = new Guid("ce9bfa9a-d34b-4368-a6a1-e44944a14459").ToString();
            var userRoleId = new Guid("854b5481-586b-4f1e-9198-66965ebedbeb").ToString();
            return new List<AppRole>
            {
                new AppRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "Administrator role with full permissions",
                    ConcurrencyStamp = "e2b569c4-ad0e-4309-8763-d1944338ef47"
                },
                new AppRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    Description = "Standard user role with limited permissions",
                    ConcurrencyStamp = "96987f15-6db4-46be-82b1-a4200d03ce43"
                }
            };
        }
        public static List<AppUser> GetUsers()
        {
            var adminUserId = new Guid("e3a3143e-0ecc-4adf-896a-0fef9401b10f").ToString();
            var userId = new Guid("97a6a4f1-eccd-4dfa-966a-46aca39d6190").ToString();

            return new List<AppUser>
            {
                new AppUser
                {
                    Id = adminUserId,
                    UserName = "admin",
                    FirstName = "Admin",
                    LastName = "User",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEJ92xHnyCJ1fvyc1H/q3o+MuYYk3RAgU9lxtdzpqO8C5QvveVGw512r5yCA0u17uqA==",
                    SecurityStamp = "ef338d50-27b8-4fbd-b5ff-04d7c2ff1c30",
                    ConcurrencyStamp = "b7874380-569c-410f-8f0a-c2c4d18c6ef4"
                },
                new AppUser
                {
                    Id = userId,
                    UserName = "user",
                    FirstName = "Standard",
                    LastName = "User",
                    NormalizedUserName = "USER",
                    Email = "user@gmail.com",
                    NormalizedEmail = "USER@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAELbmAHOQChCSgUPdacDXKvB+bfBvyHvqdXR/U2MDdUywoVFL64UmD+jERZSjKygvkg==",
                    SecurityStamp = "39f17c09-99bc-43f2-bbe2-6293e9e9f4cf",
                    ConcurrencyStamp = "bd2bf462-8af0-411a-8c4c-73c08f7a02ea"
                }
            };
        }
        public static List<IdentityUserRole<string>> GetUserRoles()
        {
            var adminRoleId = new Guid("ce9bfa9a-d34b-4368-a6a1-e44944a14459").ToString();
            var userRoleId = new Guid("854b5481-586b-4f1e-9198-66965ebedbeb").ToString();
            var adminUserId = new Guid("e3a3143e-0ecc-4adf-896a-0fef9401b10f").ToString();
            var userId = new Guid("97a6a4f1-eccd-4dfa-966a-46aca39d6190").ToString();
            return new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = userId
                }
            };
        }
    }
}
