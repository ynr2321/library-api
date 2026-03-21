using System.ComponentModel.DataAnnotations;

namespace Library.Api.DTOs;

public record CheckoutRequestDto(
    [Required]
    string BookIsbn,

    [Required]
    Guid MemberId);
