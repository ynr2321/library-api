using Library.Api.Data;
using Library.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryDbContext _db;

    public MemberRepository(LibraryDbContext db)
    {
        _db = db;
    }

    public async Task<Member?> GetByIdAsync(Guid id)
    {
        return await _db.Members.FindAsync(id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _db.Members.AnyAsync(m => m.Id == id);
    }
}
