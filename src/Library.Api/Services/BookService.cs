using Library.Api.Entities;
using Library.Api.Entities.Aggregates;
using Library.Api.Infrastructure.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library.Api.Services;

public class BookService(MongoDbContext context) : IBookService
{
    public async Task AddBookAsync(Book book)
    {
        await context.Books.InsertOneAsync(book);
    }

    public async Task<Book?> GetBookAsync(Guid id)
    {
        return await context.Books.Find(b => b.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<Book?> GetBookWithAuthorByIdAsync(Guid bookId)
    {
        var aggregate = context.Books.Aggregate()
            .Match(book => book.Id == bookId)      // Filter by the specific book Id
            .Lookup<Book, Author, Book>(
                context.Authors,                  // The author collection
                book => book.AuthorId,    // The local field in Book collection
                author => author.Id,    // The foreign field in Author collection
                book => book.Author             // The output field in Book
            )
            .Unwind<Book, Book>(book => book.Author, new AggregateUnwindOptions<Book>
            {
                PreserveNullAndEmptyArrays = true
            });

        return await aggregate.FirstOrDefaultAsync(); // Return the single book or null if not found
    }
    
    public async Task<List<Book>> GetBooksWithAuthorsAsync()
    {
        var aggregate = context.Books.Aggregate()
            .Lookup<Book, Author, Book>(
                context.Authors,                  // The author collection
                book => book.AuthorId,    // The local field in Book collection
                author => author.Id,    // The foreign field in Author collection
                book => book.Author             // The output field in Book
            )
            .Unwind<Book, Book>(book => book.Author, new AggregateUnwindOptions<Book>
            {
                PreserveNullAndEmptyArrays = true
            });

        return await aggregate.ToListAsync();
    }

    public async Task<List<Book>> GetBooksByAuthorAsync(Guid authorId)
    {
        return await context.Books.Find(b => b.AuthorId == authorId).ToListAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        await context.Books.ReplaceOneAsync(b => b.Id == book.Id, book);
    }

    public async Task DeleteBookAsync(Guid id)
    {
        await context.Books.DeleteOneAsync(b => b.Id == id);
    }

    public async Task DeleteBooksByAuthor(Guid authorId)
    {
        await context.Books.DeleteManyAsync(b => b.AuthorId == authorId);
    }
}