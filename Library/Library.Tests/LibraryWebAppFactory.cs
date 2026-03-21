using Library.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Tests;

public class LibraryWebAppFactory : WebApplicationFactory<Library.Api.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var databaseName = Guid.NewGuid().ToString();

        builder.ConfigureServices(services =>
        {
            // Remove all DbContext-related registrations so we can replace with a test database
            var descriptorsToRemove = services
                .Where(d =>
                    d.ServiceType == typeof(DbContextOptions<LibraryDbContext>) ||
                    d.ServiceType == typeof(DbContextOptions) ||
                    d.ServiceType == typeof(LibraryDbContext))
                .ToList();

            foreach (var descriptor in descriptorsToRemove)
            {
                services.Remove(descriptor);
            }

            // Add a fresh in-memory database with a unique name for test isolation
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseInMemoryDatabase(databaseName));
        });

        builder.UseEnvironment("Development");
    }
}
