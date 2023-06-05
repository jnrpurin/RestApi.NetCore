using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using WebApp.Infra.MongoDB.Repostiory;
using WebApp.Infra.MongoDB.Settings;

namespace WebApp.Infra.MongoDB
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static IServiceCollection AddMongoDBServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICompaniesRepository, CompaniesRepository>();
            services.AddSingleton<IInspectionsRepository, InspectionsRepository>();

            //services.AddSingleton<IMongoDbContext>(sp =>
            //  sp.GetRequiredService<IOptions<MongoDbContext>>().Value);
            services.AddSingleton(x =>
            {
                return configuration.GetMongoConfig();
            });

            services.AddSingleton(x =>
            {
                return configuration.GetMongoClientSettings();
            });

            services.AddTransient<IMongoDbContext, MongoDbContext>();
            services.AddSingleton(new MongoClient(configuration.GetMongoClientSettings()));

            return services;
        }
    }
}
