namespace Library.Api.Entities.Aggregates;

public class BookWithAuthor
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public Guid AuthorId { get; set; }
    public Author? Author { get; set; }
}