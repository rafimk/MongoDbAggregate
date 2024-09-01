using Library.Api.Services;

namespace Library.Api.Applications.Books;

internal sealed class GetBookDetails(IBookService bookService)
{
    public sealed record BookDetailResponse(Guid Id, string Title, DateTime PublishedDate, 
        Guid AuthorId, string AuthorName, int AuthorAge);
    
    public async Task<BookDetailResponse?> Handle(Guid bookId)
    {
        var book = await bookService.GetBookWithAuthorByIdAsync(bookId);

        return book is null ? null : new BookDetailResponse(book.Id, book.Title, book.PublishedDate, 
            book.AuthorId, book.Author?.Name, book.Author.Age);
    }
}