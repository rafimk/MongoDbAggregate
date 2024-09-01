using Library.Api.Services;

namespace Library.Api.Applications.Authors;

internal sealed class GetAuthor(IAuthorService authorService)
{
    public sealed record AuthorResponse(Guid Id, string Name, int Age);
    
    public async Task<AuthorResponse?> Handle(Guid authorId)
    {
        var author = await authorService.GetByIdAsync(authorId);

        return author is null ? null : new AuthorResponse(author.Id, author.Name, author.Age);
    }
}