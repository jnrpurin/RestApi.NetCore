﻿using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WebApp.Core.Interface;
using WebApp.Core.Services;

namespace WebApp.Core
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static IServiceCollection AddBussinessServices(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();

            return services;
        }
    }
}