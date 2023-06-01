using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Command;
using WebApp.Domain.Entity;
using WebApp.Infra.Service;

namespace WebApp.Api.Controllers
{
    [Route("produtos")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IMediator mediator;
        private readonly IRepositoryMongo<Product> repository;
        public ProductsController(IMediator mediator, IRepositoryMongo<Product> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        [HttpGet("v1/GetProduto")]
        public async Task<IActionResult> GetProd()
        {
            return Ok(await repository.GetAll());
        }

        [HttpGet("v1/GetProdut/{id}")]
        public async Task<IActionResult> GetProdId(int id)
        {
            return Ok(await repository.Get(id));
        }

        [HttpPost("v1/PostProd")]
        public async Task<IActionResult> PostProd(CreateCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("v1/PutProduto")]
        public async Task<IActionResult> PutProd(UpdateCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<IActionResult> DeleteProdId(int id)
        {
            var obj = new DeleteCommand { Id = id };
            var result = await mediator.Send(obj);
            return Ok(result);
        }
    }
}
