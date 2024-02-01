namespace WebApp.Infra.Repository
{
    //public interface IMyDatabaseRepository
    //{
    //    Task<IEnumerable<Client>> GetAllClients();
    //    Task<IEnumerable<Client>> GetClientInfoList(string firstName);
    //    Task<int> UpdateClient(Client clientInfo);
    //}

    //public class MyDatabaseRepository : DapperRepositoryBase<T>, IMyDatabaseRepository
    //{
    //    public MyDatabaseRepository(AppDatabaseContext myDatabaseContext) : base(myDatabaseContext) { }


    //    public async Task<IEnumerable<Client>> GetAllClients()
    //    {
    //        var query = $@" SELECT * FROM dbo.Client ";
    //        return await FindAsync(query);
    //    }

    //    public async Task<IEnumerable<Client>> GetClientInfoList(string firstName)
    //    {
    //        var query = $@" SELECT * FROM dbo.Client  
    //		      WHERE FirstName = '{firstName}' ";

    //        return await FindAsync(query);
    //    }


    //    public async Task<int> UpdateClient(Client clientInfo)
    //    {
    //        var script = $@" Update dbo.Client 
    //                            SET FirstName = @FirstName
    //                              , LastName = @LastName
    //                              , Age = @Age
    //                              , PhoneNumber = @PhoneNumber
    //                              , Email = @Email
    //                          WHERE ClientId = @ClientId ";

    //        var parameters = new DynamicParameters(new
    //        {
    //            clientInfo.FirstName,
    //            clientInfo.LastName,
    //            clientInfo.Age,
    //            clientInfo.PhoneNumber,    
    //            clientInfo.Email,
    //            clientInfo.ClientId
    //        });

    //        return await Execute(script, parameters);
    //    }
    //}
}