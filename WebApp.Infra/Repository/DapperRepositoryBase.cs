using Dapper;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using WebApp.Infra.Context;
using static Dapper.SqlMapper;

namespace WebApp.Infra.Repository
{
    [ExcludeFromCodeCoverage]
    public abstract class DapperRepositoryBase<T>
    {
        protected readonly IDbConnection SqlServerDbConnection;

        private const int CommandTimeout = 36000;

        protected DapperRepositoryBase(ISqlServerCustomContext customSqlContext)
        {
            SqlServerDbConnection = customSqlContext.GetConnection();
            //SqlServerDbConnection.Open();
        }

        public async Task<IEnumerable<T>> FindAsync(string query, object parameters = null)
        {
            return await QueryAsync<T>(query, parameters);
        }

        public async Task<int> Execute(string sql, object param = null, CommandType? commandType = null)
        {
            return await SqlServerDbConnection.ExecuteAsync(sql, param, null, CommandTimeout, commandType);
        }


        private async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object param = null)
        {
            return await SqlServerDbConnection.QueryAsync<TEntity>(sql, param, null, CommandTimeout);
        }
    }
}
