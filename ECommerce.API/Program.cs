
using ECommerce.BLL;
using ECommerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Scalar.AspNetCore;
using Serilog;

namespace ECommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateBootstrapLogger();

            try
            {
                Log.Information("Starting application");

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers();
                builder.Services.AddDALService(builder.Configuration);
                builder.Services
                    .AddIdentity<AppUser, AppRole>((options) => { })
                    .AddEntityFrameworkStores<DAL.AppContext>()
                    .AddDefaultTokenProviders();

                builder.Services
                    .Configure<IdentityOptions>(options =>
                    {
                        //options.Lockout.MaxFailedAccessAttempts = 5;
                        //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                        //options.Lockout.AllowedForNewUsers = true;  

                        options.SignIn.RequireConfirmedEmail = false;
                        options.SignIn.RequireConfirmedPhoneNumber = false;

                        //options.User.AllowedUserNameCharacters = "@";

                        options.User.RequireUniqueEmail = true;

                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 4;
                    });
                builder.Services.AddBLLService();

                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                builder.Services.AddOpenApi();
                var rootPath = builder.Environment.ContentRootPath;
                var staticFilesPath = Path.Combine(rootPath, "Files");
                if (!Directory.Exists(staticFilesPath))
                {
                    Directory.CreateDirectory(staticFilesPath);
                }
                builder.Services.Configure<StaticFileOptions>(options =>
                {
                    options.FileProvider = new PhysicalFileProvider(staticFilesPath);
                    options.RequestPath = "/Files";
                });
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
                });

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                    app.MapScalarApiReference();
                }
                app.UseCors("AllowAll");
                app.UseStaticFiles();
                app.UseHttpsRedirection();

                app.UseAuthentication();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
