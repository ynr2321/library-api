using Library.Api.Data;
using Library.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _db;

    public BookRepository(LibraryDbContext db)
    {
        _db = db;
    }

    public async Task<List<Book>> GetAll()
    {
        return await _db.Books
            .Include(b => b.Checkouts)
            .OrderBy(b => b.Title)
            .ToListAsync();
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        return await _db.Books.FindAsync(isbn);
    }

    public async Task<bool> ExistsAsync(string isbn)
    {
        return await _db.Books.AnyAsync(b => b.Isbn == isbn);
    }
}
