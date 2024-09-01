using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Library.Api.Infrastructure.MongoDb;

public static class ConfigureServices
{
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("mongo");
        var mongoOptions = section.BindOptions<MongoOptions>();

        if (!section.Exists())
        {
            return services;
        }

        Console.WriteLine($"MongoDB Connection String: {mongoOptions.ConnectionString}");
        
        var mongoClient = new MongoClient(mongoOptions.ConnectionString);
        var database = mongoClient.GetDatabase(mongoOptions.Database);
        var mongoDbContext = new MongoDbContext(database);
        services.AddSingleton<IMongoClient>(mongoClient);
        services.AddSingleton<MongoOptions>(mongoOptions);
        services.AddSingleton(database);
        services.AddSingleton(mongoDbContext);

        RegisterConventions();

        return services;
    }

    private static void RegisterConventions()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
        BsonSerializer.RegisterSerializer(typeof(decimal?),
            new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
        ConventionRegistry.Register("wellmate", new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String),
        }, _ => true);
    }

    public static T BindOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        => BindOptions<T>(configuration.GetSection(sectionName));

    public static T BindOptions<T>(this IConfigurationSection section) where T : new()
    {
        var options = new T();
        section.Bind(options);
        return options;
    }
}