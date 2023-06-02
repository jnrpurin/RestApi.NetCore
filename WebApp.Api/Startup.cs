using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.OpenApi.Models;
using WebApp.Core;
using WebApp.Core.Interface;
using WebApp.Infra;

namespace WebApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfireServer();
            
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage()
            );

            services.AddControllers();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp", Version = "v1" });
            });

            services.AddMemoryCache();
            services.AddBussinessServices();
            services.AddInfraServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp v1");
                });

            }

            app.UseHangfireDashboard();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var myService = app.ApplicationServices.GetService<IClientService>();
            RecurringJob.AddOrUpdate("handleRetry", () => myService.RecurringJobSample(),
                Configuration.GetSection("Time")["IntegracaoJob"]);
        }
    }
}
