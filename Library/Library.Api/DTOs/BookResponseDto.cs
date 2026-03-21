namespace Library.Api.DTOs;

public record BookResponseDto(
    string Isbn,
    string Title,
    string Author,
    int PublishedYear,
    string Genre,
    bool IsAvailable,
    Guid? CheckedOutByMemberId);
