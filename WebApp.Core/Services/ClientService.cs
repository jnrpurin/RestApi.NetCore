using WebApp.Core.Interface;
using WebApp.Domain.Models;
using WebApp.Infra.Repository;

namespace WebApp.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IMyDatabaseRepository myDatabaseRepository;

        public ClientService(IMyDatabaseRepository myDatabaseRepository)
        {
            this.myDatabaseRepository = myDatabaseRepository;
        }

        public async Task<IEnumerable<ClientInfo>> GetAllClients() => await myDatabaseRepository.GetAllClients();

        public async Task<IEnumerable<ClientInfo>> GetClientsByName(string name) => await myDatabaseRepository.GetClientInfoList(name);

        public async Task UpdateClient(ClientInfo clientInfo) => await myDatabaseRepository.UpdateClient(clientInfo);
    }
}
