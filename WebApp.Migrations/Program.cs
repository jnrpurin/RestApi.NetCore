// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;

string appSettings = $@"{System.IO.Directory.GetCurrentDirectory()}\appsettings.json";

ConfigurationBuilder configBuilder = new ConfigurationBuilder();
configBuilder
    .AddJsonFile(appsettingsRootFile)
    .AddJsonFile(appsettingsEnvFile)
    .AddEnvironmentVariables()
    .AddHashicorpVault("HashicorpVault");

var conventionPack = new MongoDB.Bson.Serialization.Conventions.ConventionPack
            {
                new MongoDB.Bson.Serialization.Conventions.CamelCaseElementNameConvention()
            };

MongoDB.Bson.Serialization.Conventions.ConventionRegistry
    .Register("AssetManagement conventions", conventionPack, x => x.FullName.StartsWith("AssetManagement"));


var cnfig = configBuilder.Build();
var migrationsRunner = new MigrationRunner(cnfig, "AssetManagement.Data.MongoDb");

migrationsRunner.Run();
