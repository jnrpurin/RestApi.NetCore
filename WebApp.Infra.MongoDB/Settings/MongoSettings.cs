using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System.Diagnostics.CodeAnalysis;

namespace WebApp.Infra.MongoDB.Settings
{
    [ExcludeFromCodeCoverage]
    public static class MongoSettings
    {
        public static MongoDbConfig GetMongoConfig(this IConfiguration configuration, string configSectionName = "MongoDb")
        {
            var mongoDbConfig = new MongoDbConfig();
            configuration.Bind(configSectionName, mongoDbConfig);

            return mongoDbConfig;
        }

        public static MongoClientSettings GetMongoClientSettings(this IConfiguration configuration, string configSectionName = "MongoDb")
        {
            var mongoDbConfig = GetMongoConfig(configuration, configSectionName);
            var internalIdentity = new MongoInternalIdentity(mongoDbConfig.Database, mongoDbConfig.User);
            var passwordEvidence = new PasswordEvidence(mongoDbConfig.Password);
            mongoDbConfig.Servers = new List<MongoServerAddress>();
            mongoDbConfig.Servers.Add(new MongoServerAddress("clustertest.7v36vpv.mongodb.net"));
            var mongoSettings = new MongoClientSettings
            {
                Credential = new MongoCredential("SCRAM-SHA-1", internalIdentity, passwordEvidence),
                Servers = mongoDbConfig.Servers,
                AllowInsecureTls = true,
                ClusterConfigurator = cb =>
                {
                    cb.Subscribe<CommandStartedEvent>(e =>
                    {

                        Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}");
                    });
                },

            };

            return mongoSettings;
        }
    }
}
