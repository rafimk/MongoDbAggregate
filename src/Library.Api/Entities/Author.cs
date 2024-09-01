using MongoDB.Bson.Serialization.Attributes;

namespace Library.Api.Entities;

public class Author : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    [BsonElement("Books")]
    public List<Book> Books { get; set; } = new List<Book>();

}