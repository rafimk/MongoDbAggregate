namespace Library.Api.Applications.Books;

internal static class BookEndpoints
{
    private const string Tag = "Books";
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("books/create", async (CreateBook.Request request, CreateBook useCase) =>
                await useCase.Handle(request))
            .WithTags(Tag);

        builder.MapPut("books/{id:guid}/update", async (Guid id, UpdateBook.Request request, UpdateBook useCase) =>
            {
                var updated = await useCase.Handle(request);
                return updated is true ? Results.NoContent() : Results.NotFound();
            })
            .WithTags(Tag);

        builder.MapGet("books/{id:guid}", async (Guid id, GetBook useCase) =>
            {
                GetBook.BookResponse? book = await useCase.Handle(id);
        
                return book is not null ? Results.Ok(book) : Results.NotFound();
            })
            .WithTags(Tag);
        
        builder.MapGet("books/{id:guid}/details", async (Guid id, GetBookDetails useCase) =>
            {
                GetBookDetails.BookDetailResponse? bookDetails = await useCase.Handle(id);
        
                return bookDetails is not null ? Results.Ok(bookDetails) : Results.NotFound();
            })
            .WithTags(Tag);
        
        builder.MapGet("books", async (GetBookAll useCase) =>
            {
                var bookDetails = await useCase.Handle();
        
                return bookDetails is not null ? Results.Ok(bookDetails) : Results.NotFound();
            })
            .WithTags(Tag);

        return builder;
    }
}