namespace Library.Api.Applications.Authors;

internal static class AuthorEndpoints
{
    private const string Tag = "Authors";
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("authors/create", async (CreateAuthor.Request request, CreateAuthor useCase) =>
                await useCase.Handle(request))
            .WithTags(Tag);

        builder.MapPut("authors/{id:guid}/update", async (Guid id, UpdateAuthor.Request request, UpdateAuthor useCase) =>
            {
                var updated = await useCase.Handle(request);
                return updated is true ? Results.NoContent() : Results.NotFound();
            })
            .WithTags(Tag);

        builder.MapGet("authors/{id:guid}", async (Guid id, GetAuthor useCase) =>
            {
                GetAuthor.AuthorResponse? author = await useCase.Handle(id);
        
                return author is not null ? Results.Ok(author) : Results.NotFound();
            })
            .WithTags(Tag);

        return builder;
    }
}