using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Command;
using WebApp.Domain.Entity;
using WebApp.Infra.Repository;

namespace WebApp.Api.Controllers
{
    [ApiController]
    [Route("[product]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IRepositoryMongo<Product> repository;
        public ProductsController(IMediator mediator, IRepositoryMongo<Product> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        [HttpGet("v1/Get")]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetAll());
        }

        [HttpGet("v1/Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await repository.Get(id));
        }

        [HttpPost("v1/Post")]
        public async Task<IActionResult> Post(CreateCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("v1/Put")]
        public async Task<IActionResult> Put(UpdateCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = new DeleteCommand { Id = id };
            var result = await mediator.Send(obj);
            return Ok(result);
        }
    }
}
