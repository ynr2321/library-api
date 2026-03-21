using Library.Api.DTOs;
using Library.Api.Enums;
using Library.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckoutsController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public CheckoutsController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    /// <summary>
    /// Checks out a book to a member.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CheckoutResponseDto>> Checkout([FromBody] CheckoutRequestDto request)
    {
        var result = await _libraryService.CheckoutBookAsync(request);

        if (!result.Success)
        {
            return result.Error switch
            {
                CheckoutError.BookNotFound => NotFound(new { message = "Book not found." }),

                CheckoutError.MemberNotFound => NotFound(new { message = "Member not found." }),

                CheckoutError.BookAlreadyCheckedOut => Conflict(new { message = "Book is already checked out." }),

                _ => StatusCode(500)
            };
        }

        return CreatedAtAction(nameof(Checkout), new { id = result.Checkout!.Id }, result.Checkout);
    }
}
