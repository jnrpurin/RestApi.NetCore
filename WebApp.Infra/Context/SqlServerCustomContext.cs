using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace WebApp.Infra.Context
{
    public interface ISqlServerCustomContext
    {
        IDbConnection GetConnection();
    }

    [ExcludeFromCodeCoverage]
    public class SqlServerCustomContext : ISqlServerCustomContext, IDisposable
    {
        private bool _disposed;
        public SqlConnection SqlDbConnection { get; set; }

        public SqlServerCustomContext(IConfiguration configuration, string system = "dbConnectionString")
        {
            SqlDbConnection = new SqlConnection(configuration.GetConnectionString(system));
        }

        public IDbConnection GetConnection()
        {
            return SqlDbConnection;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Dispose(true);
            GC.SuppressFinalize(this);
            _disposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (SqlDbConnection != null)
                {
                    SqlDbConnection.Dispose();
                    SqlDbConnection = null;
                }
            }
        }
    }
}
