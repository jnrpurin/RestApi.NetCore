using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WebApp.Infra.Context;
using WebApp.Infra.Repository;

namespace WebApp.Infra
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static IServiceCollection AddSqlServerDbSession(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<MyDatabaseContext>();

            services.AddScoped<IMyDatabaseRepository, MyDatabaseRepository>();

            return services;
        }
    }
}
