using Library.Api.Services;

namespace Library.Api.Applications.Authors;

internal sealed class UpdateAuthor(IAuthorService authorService)
{
    public sealed record Request(Guid Id, string Name, int Age);

    public async Task<bool> Handle(Request request)
    {
        var author = await authorService.GetByIdAsync(request.Id);

        if (author == null)
        {
            return false;
        }

        author.Name = request.Name;
        author.Age = request.Age;
  
        await authorService.UpdateAuthorAsync(author);
        
        return true;
    }
}