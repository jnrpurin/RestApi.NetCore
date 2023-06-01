﻿using WebApp.Core.Interface;
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

        public async Task<IEnumerable<ClientInfo>> GetClientsByName(string name) => await myDatabaseRepository.GetClientInfoList(name);


        public async Task<int> UpdateClient(ClientInfo clientInfo)
        {
            return await myDatabaseRepository.UpdateClient(clientInfo);
        }
    }
}
