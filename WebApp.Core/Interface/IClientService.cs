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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientInfo"></param>
        /// <returns></returns>
        Task<int> UpdateAClient(ClientInfo clientInfo);

        /// <summary>
        /// Sample recurring job exec
        /// </summary>
        /// <returns></returns>
        Task RecurringJobSample();
    }
}
