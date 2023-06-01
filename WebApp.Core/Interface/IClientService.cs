using WebApp.Domain.Models;

namespace WebApp.Core.Interface
{
    public interface IClientService
    {
        /// <summary>
        /// Search for clients with the specific name provided
        /// </summary>
        /// <param name="name">Client name</param>
        /// <returns>List of clients</returns>
        Task<IEnumerable<ClientInfo>> GetClientsByName(string name);

        Task<int> UpdateClient(ClientInfo clientInfo);
    }
}
