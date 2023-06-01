using Microsoft.OpenApi.Models;
using WebApp.Core;
using WebApp.Core.Interface;
using WebApp.Core.Services;
using WebApp.Infra;
using WebApp.Infra.Context;
using WebApp.Infra.Service;

namespace WebApp.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1 test", new OpenApiInfo { Title = "WebApp.API", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insira um token válido",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDatabaseCustomer"));
            services.AddSingleton<RepositoryMongo>();

            services.AddSingleton<HttpClient>();
            services.AddTransient<IClientAutoUpdateService, ClientAutoUpdateService>();

            //var options = new MongoStorageOptions
            //{
            //    MigrationOptions = new MongoMigrationOptions
            //    {
            //        MigrationStrategy = new MigrateMongoMigrationStrategy(),
            //        BackupStrategy = new NoneMongoBackupStrategy()
            //    }
            //};

            //services.AddHangfire(config =>
            //{
            //    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
            //    config.UseSimpleAssemblyNameTypeSerializer();
            //    config.UseRecommendedSerializerSettings();
            //    config.UseMongoStorage(@"mongodb+srv://admin:27HSurWhyIW5QCan@clustertest.7v36vpv.mongodb.net/sample_analytics?authSource=admin", options);
            //});
            //services.AddHangfireServer();

            services.AddSqlServerDbSession();
            services.AddBussinessServices();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("./v1/swagger.json", "WebApp.Api v1");
                }); 
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseHangfireDashboard();

            //var clientUpdate = app.ApplicationServices.GetService<IClientAutoUpdateService>();
            //BackgroundJob.Enqueue(() => clientUpdate.ClientAutoUpdate());
            //RecurringJob.AddOrUpdate("ClientAutoUpdate", () => clientUpdate.ClientAutoUpdate(), "5 * * * *");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
