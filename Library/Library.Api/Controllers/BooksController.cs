using Library.Api.DTOs;
using Library.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public BooksController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    /// <summary>
    /// Returns all books in the library with their current availability status.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<BookResponseDto>>> GetAll()
    {
        var books = await _libraryService.GetAllBooksAsync();
        return Ok(books);
    }
}
