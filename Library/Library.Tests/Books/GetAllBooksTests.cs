using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Library.Api.DTOs;
using Xunit;

namespace Library.Tests.Books;

public class GetAllBooksTests : IClassFixture<LibraryWebAppFactory>
{
    private readonly HttpClient _client;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public GetAllBooksTests(LibraryWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllBooks_ReturnsAllSeededBooks()
    {
        // we know we seeded 8 books, so check endpoint doenst blow up when called and we can read 8 books from the response
        var response = await _client.GetAsync("/api/books");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var books = await response.Content.ReadFromJsonAsync<List<BookResponseDto>>(JsonOptions);

        Assert.NotNull(books);
        Assert.Equal(8, books.Count);
    }
}
