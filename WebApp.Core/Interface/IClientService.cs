using WebApp.Domain.Models;
using WebApp.Infra.Repository;

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

        /// <summary>
        /// Update client
        /// </summary>
        /// <param name="clientInfo">Client data</param>
        Task UpdateClient(ClientInfo clientInfo);
    }
}
