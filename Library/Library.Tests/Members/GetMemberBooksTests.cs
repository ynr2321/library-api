using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Library.Api.Data;
using Library.Api.DTOs;
using Xunit;

namespace Library.Tests.Members;

public class GetMemberBooksTests : IClassFixture<LibraryWebAppFactory>
{
    private readonly HttpClient _client;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public GetMemberBooksTests(LibraryWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetMemberBooks_ReturnsCheckedOutBooksForMember()
    {
        // We know Alice already has 2 books checked out from the seed data (ISBNs 2 and 6)
        var response = await _client.GetAsync($"/api/members/{SeedData.MemberAliceId}/books");

        // Check didnt blow up
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Check response shows she owns the expected books
        var result = await response.Content.ReadFromJsonAsync<MemberBooksResponseDto>(JsonOptions);

        Assert.NotNull(result);
        Assert.Equal(SeedData.MemberAliceId, result.MemberId);
        Assert.Equal(2, result.Books.Count);

        var checkedOutIsbns = result.Books.Select(b => b.Isbn).OrderBy(i => i).ToList();
        Assert.Equal(["2", "6"], checkedOutIsbns);
    }
}
