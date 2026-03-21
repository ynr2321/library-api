using Library.Api.Entities;

namespace Library.Api.Repositories;

public interface ICheckoutRepository
{
    Task<Checkout?> GetActiveCheckoutForBookAsync(string bookIsbn);
    Task<List<Checkout>> GetActiveCheckoutsByMemberAsync(Guid memberId);
    Task<Checkout> CreateAsync(Checkout checkout);
    Task UpdateAsync(Checkout checkout);
}
