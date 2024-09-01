using MongoDB.Bson.Serialization.Attributes;

namespace Library.Api.Entities;

public class Book : IEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public DateTime PublishedDate { get; set; }
    public Guid AuthorId { get; set; }
    [BsonIgnoreIfNull]
    public Author? Author { get; set; }
}