using Library.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Data;

public static class SeedData
{
    public static readonly Guid MemberAliceId = Guid.Parse("a1111111-1111-1111-1111-111111111111");
    public static readonly Guid MemberBobId = Guid.Parse("b2222222-2222-2222-2222-222222222222");
    public static readonly Guid MemberCarolId = Guid.Parse("c3333333-3333-3333-3333-333333333333");

    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

        // just for saftey - shouldn't be needed while using In Memory db but would be if swapped out for a container with a volume
        if (await db.Books.AnyAsync()) return;

        // Create some books (using short ISBN to make it easier to manually test with scalar)
        var books = new List<Book>
        {
            new() { Isbn = "1", Title = "Fake Book 1", Author = "Author One", PublishedYear = 2001, Genre = "Fiction" },
            new() { Isbn = "2", Title = "Dependency Injection", Author = "Mark Seemann", PublishedYear = 2002, Genre = "Software Development" },
            new() { Isbn = "3", Title = "Fake Book 3", Author = "Author Three", PublishedYear = 2003, Genre = "Sci-Fi" },
            new() { Isbn = "4", Title = "Halo: The Fall of Reach", Author = "Eric Nylund", PublishedYear = 2001, Genre = "Sci-Fi" },
            new() { Isbn = "5", Title = "Fake Book 5", Author = "Author Five", PublishedYear = 2005, Genre = "Mystery" },
            new() { Isbn = "6", Title = "Why Yusef Fits the Role", Author = "Author Six", PublishedYear = 2026, Genre = "Career" },
            new() { Isbn = "7", Title = "Fake Book 7", Author = "Author Seven", PublishedYear = 2007, Genre = "Horror" },
            new() { Isbn = "8", Title = "Fake Book 8", Author = "Author Eight", PublishedYear = 2008, Genre = "Adventure" },
        };

        // Add some members
        var members = new List<Member>
        {
            new() { Id = MemberAliceId, FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com", MemberSince = new DateTime(2022, 1, 15) },
            new() { Id = MemberBobId, FirstName = "Bob", LastName = "Smith", Email = "bob@example.com", MemberSince = new DateTime(2023, 6, 1) },
            new() { Id = MemberCarolId, FirstName = "Carol", LastName = "Williams", Email = "carol@example.com", MemberSince = new DateTime(2024, 3, 10) },
        };

        // Add some checkouts
        var checkouts = new List<Checkout>
        {
            new() { BookIsbn = "2", MemberId = MemberAliceId, CheckoutDate = DateTime.UtcNow.AddDays(-7) },
            new() { BookIsbn = "6", MemberId = MemberAliceId, CheckoutDate = DateTime.UtcNow.AddDays(-3) },
        };

        db.Books.AddRange(books);
        db.Members.AddRange(members);
        db.Checkouts.AddRange(checkouts);
        await db.SaveChangesAsync();
    }
}
