using Library.Api.Services;

namespace Library.Api.Applications.Books;

internal sealed class GetBookAll(IBookService bookService)
{
    public sealed record BookResponse(Guid Id, string Title, DateTime PublishedDate, Guid AuthorId,
        string AuthorName, int AuthorAge);
    
    public async Task<List<BookResponse>?> Handle()
    {
        var book = await bookService.GetBooksWithAuthorsAsync();

        return book is null ? null : book.Select(x => new BookResponse(x.Id, x.Title, x.PublishedDate, x.AuthorId,
            x.Author.Name, x.Author.Age)).ToList();
    }
}