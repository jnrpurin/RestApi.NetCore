using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WebApp.Infra.MongoDB.Repostiory;

namespace WebApp.Infra.MongoDB
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static IServiceCollection AddMongoDBServices(this IServiceCollection services)
        {
            services.AddSingleton<ICompaniesRepository, CompaniesRepository>();

            return services;
        }
    }
}
