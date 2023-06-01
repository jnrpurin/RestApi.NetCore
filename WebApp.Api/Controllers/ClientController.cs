using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Interface;
using WebApp.Domain.Models;
using WebApp.Domain.Request;

namespace WebApp.Api.Controllers
{
    [ApiController]
    [Route("[client]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> logger;
        private readonly IClientService clientService;

        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            this.logger = logger;
            this.clientService = clientService;
        }


        [HttpGet("v1/GetClientList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClient([FromBody] ClientRequest clientRequest, [FromHeader] int id)
        {
            var token = Request.Headers["Authorization"];
            if (token.Count <= 0)
                return Unauthorized();

            if (string.IsNullOrEmpty(id))
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