using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace WebApp.Infra.Context
{
    [ExcludeFromCodeCoverage]
    public class MyDatabaseContext : SqlServerCustomContext
    {
        public MyDatabaseContext(IConfiguration configuration) : base(configuration, "MyDatabase") { }
    }
}
