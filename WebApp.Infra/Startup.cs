using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WebApp.Infra.Context;
using WebApp.Infra.Repository;

namespace WebApp.Infra
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddTransient<MyDatabaseContext>();

            services.AddTransient<IMyDatabaseRepository, MyDatabaseRepository>();

            return services;
        }
    }
}
