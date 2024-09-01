using Library.Api.Entities;
using Library.Api.Services;

namespace Library.Api.Applications.Authors;

internal sealed class CreateAuthor(IAuthorService authorService)
{
    public sealed record Request(string Name, int Age);

    public async Task<Guid> Handle(Request request)
    {
        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Age = request.Age
        };

        await authorService.AddAuthorAsync(author);
        
        return author.Id;
    }
}