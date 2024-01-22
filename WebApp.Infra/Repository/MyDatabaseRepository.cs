using Dapper;
using WebApp.Domain.Models;
using WebApp.Infra.Context;

namespace WebApp.Infra.Repository
{
    public interface IMyDatabaseRepository
    {
        Task<IEnumerable<ClientInfo>> GetAllClients();
        Task<IEnumerable<ClientInfo>> GetClientInfoList(string firstName);
        Task<int> UpdateClient(ClientInfo clientInfo);
    }

    public class MyDatabaseRepository : DapperRepositoryBase<ClientInfo>, IMyDatabaseRepository
    {
        public MyDatabaseRepository(MyDatabaseContext myDatabaseContext) : base(myDatabaseContext) { }

        
        public async Task<IEnumerable<ClientInfo>> GetAllClients()
        {
            var query = $@" SELECT * FROM dbo.Client ";
            return await FindAsync(query);
        }

        public async Task<IEnumerable<ClientInfo>> GetClientInfoList(string firstName)
        {
            var query = $@" SELECT * FROM dbo.Client  
						      WHERE FirstName = '{firstName}' ";

            return await FindAsync(query);
        }


        public async Task<int> UpdateClient(ClientInfo clientInfo)
        {
            var script = $@" Update dbo.Client 
                                SET FirstName = @FirstName
                                  , LastName = @LastName
                                  , Age = @Age
                                  , PhoneNumber = @PhoneNumber
                                  , Email = @Email
                              WHERE ClientId = @ClientId ";

            var parameters = new DynamicParameters(new
            {
                clientInfo.FirstName,
                clientInfo.LastName,
                clientInfo.Age,
                clientInfo.PhoneNumber,    
                clientInfo.Email,
                clientInfo.ClientId
            });

            return await Execute(script, parameters);
        }
    }
}