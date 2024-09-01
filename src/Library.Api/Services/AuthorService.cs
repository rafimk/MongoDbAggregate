using Library.Api.Entities;
using Library.Api.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace Library.Api.Services;

public class AuthorService(MongoDbContext context) : IAuthorService
{
    public async Task AddAuthorAsync(Author author)
    {
        await context.Authors.InsertOneAsync(author);
    }
    public async Task<Author> GetByIdAsync(Guid id) => 
        await context.Authors.Find(a => a.Id == id).FirstOrDefaultAsync();

    public async Task AddBookToAuthorAsync(Guid authorId, Book book)
    {
        var author = await GetByIdAsync(authorId);

        book.AuthorId = authorId;
        author.Books.Add(book);

        await context.Authors.ReplaceOneAsync(a => a.Id == authorId, author);
    }

    public async Task<List<Author>> GetAllAuthorsAsync() 
        => await context.Authors.Find(_ => true).ToListAsync();

    public async Task UpdateAuthorAsync(Author author)
    {
        await context.Authors.ReplaceOneAsync(a => a.Id == author.Id, author);
    }

    public async Task DeleteAuthorAsync(Guid id)
    {
        await context.Authors.DeleteOneAsync(a => a.Id == id);
    }
}