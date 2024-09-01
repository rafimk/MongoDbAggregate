using Library.Api.Entities;
using MongoDB.Driver;

namespace Library.Api.Infrastructure.MongoDb;

public class MongoDbContext(IMongoDatabase database)
{
    public IMongoCollection<Author> Authors => database.GetCollection<Author>("Authors");
    public IMongoCollection<Book> Books => database.GetCollection<Book>("Books");
}