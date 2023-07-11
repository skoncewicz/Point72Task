using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Point72.LibraryApi.Database;

namespace Point72.LibraryApi.IntegrationTests;

internal class SqlLiteWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<LibraryDbContext>)
            );

            if (descriptor != null) 
                services.Remove(descriptor);

            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlite("DataSource=:memory:");
            });
        });
    }
}