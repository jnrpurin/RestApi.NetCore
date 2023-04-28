using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Interface;
using WebApp.Domain.Models;
using WebApp.Domain.Request;

namespace WebApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> logger;
        private readonly IClientService clientService;

        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            this.logger = logger;
            this.clientService = clientService;
        }

        [HttpGet(Name = "GetClient")]
        public async Task<IActionResult> GetClientList(ClientRequest clientRequest)
        {
            logger.LogInformation($"Getting list of clients with name {clientRequest.FirstName}.");

            var clientList = await clientService.GetClientsByName(clientRequest.FirstName);
            if (clientList == null)
            {
                var resultNull = $"The client name {clientRequest.FirstName} did not return any value.";
                logger.LogWarning(resultNull);
                return BadRequest(new ErrorResponse { Code = "2", Message = resultNull });
            }
            
            return StatusCode(200, clientList);
        }
    }
}