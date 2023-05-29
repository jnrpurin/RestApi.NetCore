using Hangfire;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using System.Globalization;
using WebApp.Core.Interface;
using WebApp.Core.Services;
using WebApp.Infra;
using WebApp.Core;
using WebApp.Infra.Context;
using WebApp.Infra.Service;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;

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
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp.API", Version = "v1" });
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

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("pt-BR");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
                options.SetDefaultCulture("pt-BR");
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDatabaseCustomer"));
            services.AddSingleton<CustomerService>();

            services.AddSingleton<HttpClient>();
            services.AddTransient<IClientAutoUpdateService, ClientAutoUpdateService>();

            //services.AddHangfire(configuration => configuration
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UseSqlServerStorage(Configuration.GetConnectionString("MyDataBase"))
            //);

            //var mongoConnection = Configuration.GetConnectionString("MongoDatabaseCustomer:ConnectionString");
            var options = new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                    BackupStrategy = new NoneMongoBackupStrategy()
                }
            };

            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
                config.UseSimpleAssemblyNameTypeSerializer();
                config.UseRecommendedSerializerSettings();
                config.UseMongoStorage(@"mongodb://admin:27HSurWhyIW5QCan@ac-n6zwu7z-shard-00-02.7v36vpv.mongodb.net:27017/sample_analytics?authSource=admin", options);
                //mongodb://user:password@ipaddress:port/DbName?authSource=admin
            });
            services.AddHangfireServer();

            services.AddSqlServerDbSession();
            services.AddBussinessServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard();

            var clientUpdate = app.ApplicationServices.GetService<IClientAutoUpdateService>();

            //TODO: add quando metodo ClientAutoUpdate() estiver desenvolvido
            //RecurringJob.AddOrUpdate("ClientAutoUpdate", () => clientUpdate.ClientAutoUpdate(), "5 * * * *");
            //BackgroundJob.Enqueue(() => clientUpdate.ClientAutoUpdate());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
