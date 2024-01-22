using WebApp.Domain.Models;

namespace WebApp.Core.Interface
{
    public interface IClientService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ClientInfo>> GetAllClients();
        
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
