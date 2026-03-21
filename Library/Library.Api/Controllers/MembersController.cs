using Library.Api.DTOs;
using Library.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public MembersController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    /// <summary>
    /// Returns all books currently checked out by the specified member.
    /// </summary>
    [HttpGet("{memberId:guid}/books")]
    public async Task<ActionResult<MemberBooksResponseDto>> GetMemberBooks(Guid memberId)
    {
        var result = await _libraryService.GetMemberBooksAsync(memberId);

        if (result is null) return NotFound();

        return Ok(result);
    }
}
