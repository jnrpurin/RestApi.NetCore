using Microsoft.Extensions.Logging;
using WebApp.Core.Interface;
using WebApp.Domain.Models;
using WebApp.Infra.Repository;

namespace WebApp.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IMyDatabaseRepository myDatabaseRepository;
        private readonly ILogger<ClientService> logger;

        public ClientService(IMyDatabaseRepository myDatabaseRepository, ILogger<ClientService> logger)
        {
            this.myDatabaseRepository = myDatabaseRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<ClientInfo>> GetClientsByName(string name) => await myDatabaseRepository.GetClientInfoList(name);

        public async Task<int> UpdateAClient(ClientInfo clientInfo) => await myDatabaseRepository.UpdateClient(clientInfo);

        public async Task RecurringJobSample()
        {
            try
            {
                logger.LogInformation("{nameof} ---Starting RecurringJobSample---", nameof(ClientService));
                await Task.Run(() => { Console.WriteLine("exec your recurring job"); });
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error {error}", nameof(ClientService));
                throw new Exception("ClientService Error", e);
            }

        }
    }
}
