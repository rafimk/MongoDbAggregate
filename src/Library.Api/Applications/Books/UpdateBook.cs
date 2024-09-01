using Library.Api.Services;

namespace Library.Api.Applications.Books;

internal sealed class UpdateBook(IBookService bookService)
{
    public sealed record Request(Guid Id, string Title, DateTime PublishedDate, Guid AuthorId);

    public async Task<bool> Handle(Request request)
    {
        var book = await bookService.GetBookAsync(request.Id);

        if (book == null)
        {
            return false;
        }

        book.Title = request.Title;
        book.PublishedDate = request.PublishedDate;
        book.AuthorId = request.AuthorId;
  
        await bookService.UpdateBookAsync(book);
        
        return true;
    }
}