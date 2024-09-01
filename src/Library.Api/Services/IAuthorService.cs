using Library.Api.Entities;

namespace Library.Api.Services;

public interface IAuthorService
{
    Task AddAuthorAsync(Author author);
    Task<Author> GetByIdAsync(Guid id);
    Task AddBookToAuthorAsync(Guid authorId, Book book);
    Task<List<Author>> GetAllAuthorsAsync();
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(Guid id);
}