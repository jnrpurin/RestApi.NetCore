using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WebApp.Domain.Entity;
using WebApp.Infra.Context;
using WebApp.Infra.Repository;
using WebApp.Infra.Service;

namespace WebApp.Infra
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static IServiceCollection AddSqlServerDbSession(this IServiceCollection services)
        {
            services.AddTransient<MyDatabaseContext>();

            services.AddSingleton<IRepositoryMongo<Product>, RepositoryMongo>();

            services.AddScoped<IMyDatabaseRepository, MyDatabaseRepository>();

            return services;
        }
    }
}
