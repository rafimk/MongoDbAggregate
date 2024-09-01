using Library.Api.Services;

namespace Library.Api.Applications.Books;

internal sealed class GetBook(IBookService bookService)
{
    public sealed record BookResponse(Guid Id, string Title, DateTime PublishedDate, Guid AuthorId);
    
    public async Task<BookResponse?> Handle(Guid authorId)
    {
        var book = await bookService.GetBookAsync(authorId);

        return book is null ? null : new BookResponse(book.Id, book.Title, book.PublishedDate, book.AuthorId);
    }
}