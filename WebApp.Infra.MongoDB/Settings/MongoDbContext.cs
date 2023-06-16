using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace WebApp.Infra.MongoDB.Settings
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);

        MongoClient GetMongoClient();
    }

    [ExcludeFromCodeCoverage]
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase mongoDatabase;
        protected readonly MongoClient mongoClient;

        public MongoDbContext(MongoClient client, MongoDbConfig mongoDatabaseName)
        {
            mongoClient = client;
            mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName.Database);
        }

        public IMongoCollection<T> GetCollection<T>() => mongoDatabase.GetCollection<T>(typeof(T).Name);

        public IMongoCollection<T> GetCollection<T>(string collectioName)
        {
            return mongoDatabase.GetCollection<T>(collectioName);
        }

        public MongoClient GetMongoClient() => mongoClient;
    }

    [ExcludeFromCodeCoverage]
    public class MongoDbConfig
    {
        public string? Database { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public List<MongoServerAddress> Servers { get; set; } = null!;
    }
}
