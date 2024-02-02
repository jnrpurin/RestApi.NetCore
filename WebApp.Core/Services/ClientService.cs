using Microsoft.EntityFrameworkCore;
using WebApp.Core.Interface;
using WebApp.Domain.Models;
using WebApp.Domain.Request;
using WebApp.Infra.Context;

namespace WebApp.Core.Services
{
    internal class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        public ClientService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<ServiceResponse<List<Client>>> CreateClient(Client newClient)
        {
            ServiceResponse<List<Client>> serviceResponse = new();
            try
            {
                if (newClient == null) 
                { 
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    return serviceResponse;
                }

                _context.Clients.Add(newClient);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Clients.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Client>>> DeleteClient(int id)
        {
            ServiceResponse<List<Client>> serviceResponse = new();
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (client == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Client not found!";
                    return serviceResponse;
                }
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Clients.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Client>> GetClientById(int id)
        {
            ServiceResponse<Client> serviceResponse = new();
            try
            {
                serviceResponse.Data = await _context.Clients.FirstOrDefaultAsync(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Client>>> GetClients()
        {
            ServiceResponse<List<Client>> serviceResponse = new();
            try
            {
                serviceResponse.Data = await _context.Clients.ToListAsync();
                if (!serviceResponse.Data.Any())
                    serviceResponse.Message = "No data found!";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Client>> UpdateClient(Client editClient)
        {
            ServiceResponse<Client> serviceResponse = new();
            try
            {
                var client = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(editClient.Id));
                if (client == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Client not found!";
                    return serviceResponse;
                }
                _context.Clients.Update(editClient);
                await _context.SaveChangesAsync();

                serviceResponse.Data = editClient;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
