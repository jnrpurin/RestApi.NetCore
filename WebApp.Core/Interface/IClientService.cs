using WebApp.Domain.Models;
using WebApp.Domain.Request;

namespace WebApp.Core.Interface
{
    public interface IClientService
    {
        Task<ServiceResponse<List<Client>>> GetClients();
        Task<ServiceResponse<Client>> GetClientById(int id);
        Task<ServiceResponse<List<Client>>> CreateClient(Client newClient);
        Task<ServiceResponse<Client>> UpdateClient(Client editClient);
        Task<ServiceResponse<List<Client>>> DeleteClient(int id);
    }
}
