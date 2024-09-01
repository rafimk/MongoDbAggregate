using Library.Api.Entities;
using Library.Api.Services;

namespace Library.Api.Applications.Books;

internal sealed class CreateBook(IBookService bookService)
{
    public sealed record Request(string Title, DateTime PublishedDate, Guid AuthorId);
    
    public async Task<Guid> Handle(Request request)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            AuthorId = request.AuthorId
        };

        await bookService.AddBookAsync(book);

        return book.Id;
    }
}
