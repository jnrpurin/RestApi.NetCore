using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Interface;
using WebApp.Domain.Models;

namespace WebApp.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> logger;
        private readonly IClientService clientService;

        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            this.logger = logger;
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await clientService.GetClients());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            return Ok(await clientService.GetClientById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            return Ok(await clientService.CreateClient(client));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] Client client)
        {
            return Ok(await clientService.UpdateClient(client));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            return Ok(await clientService.DeleteClient(id));
        }

    }
}