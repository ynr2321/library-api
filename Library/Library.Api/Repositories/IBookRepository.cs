using Library.Api.Entities;

namespace Library.Api.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAll();
    Task<Book?> GetByIsbnAsync(string isbn);
    Task<bool> ExistsAsync(string isbn);
}
