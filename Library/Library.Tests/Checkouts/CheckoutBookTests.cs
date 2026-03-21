using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Library.Api.Data;
using Library.Api.DTOs;
using Xunit;

namespace Library.Tests.Checkouts;

public class CheckoutBookTests : IClassFixture<LibraryWebAppFactory>
{
    private readonly HttpClient _client;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public CheckoutBookTests(LibraryWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    /* 
     * It would be best to have seperate tests for:
     * - succesfuly check out a book that isnt already checked out
     * - get appropriate error for trying to check out non-existent book
     * - get appropriate error for trying to check out book to a non-existent member
     * - get appropriate error for trying to check out book that is already checked out
     * 
     * However, chose to violate seperation concerns a little bit and cover more than one bit of functionality per test method
     * to quickly cover important bits as not meant to spend ages on this
     */
    [Fact]
    public async Task Checkout_AvailableBook_IsReflectedInGetAllBooks()
    {
        // Check out an available book ISBN "1" to Bob
        var checkoutRequest = new CheckoutRequestDto("1", SeedData.MemberBobId);
        var checkoutResponse = await _client.PostAsJsonAsync("/api/checkouts", checkoutRequest);

        Assert.Equal(HttpStatusCode.Created, checkoutResponse.StatusCode);

        var checkoutResult = await checkoutResponse.Content.ReadFromJsonAsync<CheckoutResponseDto>(JsonOptions);

        Assert.NotNull(checkoutResult);
        Assert.Equal("1", checkoutResult.BookIsbn);
        Assert.Equal(SeedData.MemberBobId, checkoutResult.MemberId);

        // Verify Bob now has book 1 via GET /api/books
        var booksResponse = await _client.GetAsync("/api/books");
        var books = await booksResponse.Content.ReadFromJsonAsync<List<BookResponseDto>>(JsonOptions);

        Assert.NotNull(books);

        var checkedOutBook = books.Single(b => b.Isbn == "1");
        Assert.False(checkedOutBook.IsAvailable);
        Assert.Equal(SeedData.MemberBobId, checkedOutBook.CheckedOutByMemberId);
    }
}
