using Library.Api.Entities;
using Library.Api.Entities.Aggregates;

namespace Library.Api.Services;

public interface IBookService
{
    Task AddBookAsync(Book book);
    Task<Book?> GetBookAsync(Guid id);
    Task<Book?> GetBookWithAuthorByIdAsync(Guid bookId);
    Task<List<Book>> GetBooksByAuthorAsync(Guid authorId);
    Task<List<Book>> GetBooksWithAuthorsAsync();
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(Guid id);
    Task DeleteBooksByAuthor(Guid authorId);
}