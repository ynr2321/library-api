using Library.Api.Data;
using Library.Api.Repositories;
using Library.Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Library.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // EF Core in-memory database
        builder.Services.AddDbContext<LibraryDbContext>(options =>
            options.UseInMemoryDatabase("LibraryDb"));

        // Add scoped services to DI container
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IMemberRepository, MemberRepository>();
        builder.Services.AddScoped<ICheckoutRepository, CheckoutRepository>();
        builder.Services.AddScoped<ILibraryService, LibraryService>();


        builder.Services.AddControllers();

        // Adding Open API / Scalar support so tester doesnt have to mess around with postman etc
        // modified launch settings so should open on <host:port>/scalar/v1
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.MapControllers();

        // Seeding some books, members and checkouts since requirements didnt ask for a way to add new books
        await SeedData.InitializeAsync(app.Services);

        app.Run();
    }
}
