namespace Library.Api.DTOs;

public record MemberBooksResponseDto(
    Guid MemberId,
    string MemberName,
    IReadOnlyList<MemberBookDto> Books);

public record MemberBookDto(
    string Isbn,
    string Title,
    string Author,
    DateTime CheckoutDate);
