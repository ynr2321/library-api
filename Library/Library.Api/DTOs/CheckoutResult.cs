using Library.Api.Enums;

namespace Library.Api.DTOs;

public class CheckoutResult
{
    public bool Success { get; }
    public CheckoutResponseDto? Checkout { get; }
    public CheckoutError? Error { get; }

    public CheckoutResult(bool success, CheckoutResponseDto? checkout, CheckoutError? error)
    {
        Success = success;
        Checkout = checkout;
        Error = error;
    }
}
