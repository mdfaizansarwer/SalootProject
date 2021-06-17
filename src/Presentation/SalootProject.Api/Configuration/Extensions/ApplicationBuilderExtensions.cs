using Data.DataInitializer;
using Date;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalootProject.Api.Midllewares;

namespace SalootProject.Api.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }

        public static void IntializeDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();

            var dataInitializer = scope.ServiceProvider.GetService<DataInitializer>();
            dataInitializer.InstallRequierdData();
        }
    }
}