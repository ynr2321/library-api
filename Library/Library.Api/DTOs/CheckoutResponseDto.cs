namespace Library.Api.DTOs;

public record CheckoutResponseDto(
    int Id,
    string BookIsbn,
    string BookTitle,
    Guid MemberId,
    string MemberName,
    DateTime CheckoutDate);
