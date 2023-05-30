using Microsoft.Extensions.Logging;
using WebApp.Core.Interface;

namespace WebApp.Core.Services
{
    public class ClientAutoUpdateService : IClientAutoUpdateService
    {
        private readonly ILogger<ClientAutoUpdateService> logger;
        public ClientAutoUpdateService(ILogger<ClientAutoUpdateService> logger)
        {
            this.logger = logger;
        }

        public async Task ClientAutoUpdate()
        {
            try
            {
                logger.LogInformation($" ClientAutoUpdate() - Inicio");
                await Task.Run(() => { Console.WriteLine($"Usando hangfire - {DateTime.Now}"); });
                
                //TODO: implement auto update from client data
            }
            catch (Exception ex)
            {
                var error = $"ClientAutoUpdate() - {ex.Message}";
                logger.LogError(error);
                throw new Exception(error, ex);
            }

        }

    }
}
