using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Interface;
using WebApp.Domain.Models;
using WebApp.Domain.Request;

namespace WebApp.Api.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> logger;
        private readonly IClientService clientService;

        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            this.logger = logger;
            this.clientService = clientService;
        }


        [HttpGet("v1/GetClientList")]
        public async Task<IActionResult> GetClientList([FromQuery] ClientRequest clientRequest)
        {
            var token = Request.Headers["Authorization"];
            if (token.Count <= 0)
                return Unauthorized();

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

        [HttpPost("v1/UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody] ClientRequest clientRequest, [FromHeader] int id)
        {
            var token = Request.Headers["Authorization"];
            if (token.Count <= 0)
                return Unauthorized();

            if (id <= 0)
                return BadRequest("Identificator not informed");

            
            await clientService.UpdateClient(new ClientInfo
            {
                ClientId = id,
                Age = clientRequest.Age,
                FirstName = clientRequest.FirstName,
                LastName = clientRequest.LastName,
                Email = clientRequest.Email
            });

            return Ok();
        }
    }
}