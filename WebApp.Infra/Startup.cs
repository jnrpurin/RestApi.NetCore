using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WebApp.Infra.Context;

namespace WebApp.Infra
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static IServiceCollection AddSqlServerDbSession(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("AppDataBase"));
            });

            return services;
        }
    }
}
