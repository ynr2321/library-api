using Library.Api.Data;
using Library.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Repositories;

public class CheckoutRepository : ICheckoutRepository
{
    private readonly LibraryDbContext _db;

    public CheckoutRepository(LibraryDbContext db)
    {
        _db = db;
    }

    public async Task<Checkout?> GetActiveCheckoutForBookAsync(string bookIsbn)
    {
        return await _db.Checkouts
            .Include(c => c.Book)
            .Include(c => c.Member)
            .FirstOrDefaultAsync(c => c.BookIsbn == bookIsbn);
    }

    public async Task<List<Checkout>> GetActiveCheckoutsByMemberAsync(Guid memberId)
    {
        return await _db.Checkouts
            .Include(c => c.Book)
            .Where(c => c.MemberId == memberId)
            .OrderByDescending(c => c.CheckoutDate)
            .ToListAsync();
    }

    public async Task<Checkout> CreateAsync(Checkout checkout)
    {
        _db.Checkouts.Add(checkout);
        await _db.SaveChangesAsync();
        return checkout;
    }

    public async Task UpdateAsync(Checkout checkout)
    {
        _db.Checkouts.Update(checkout);
        await _db.SaveChangesAsync();
    }
}
