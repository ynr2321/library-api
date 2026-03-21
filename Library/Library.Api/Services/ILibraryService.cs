using Library.Api.DTOs;

namespace Library.Api.Services;

public interface ILibraryService
{
    Task<List<BookResponseDto>> GetAllBooksAsync();
    Task<MemberBooksResponseDto?> GetMemberBooksAsync(Guid memberId);
    Task<CheckoutResult> CheckoutBookAsync(CheckoutRequestDto request);
}
