using Library.Api.Entities;

namespace Library.Api.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
