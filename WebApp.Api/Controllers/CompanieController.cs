using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Services;
using WebApp.Domain.MongoEntities;

namespace WebApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanieController : ControllerBase
    {
        private readonly CompaniesService _companieService;

        public CompanieController(CompaniesService companieService) =>
            _companieService = companieService;

        [HttpGet]
        public async Task<IEnumerable<Companies>> GetCompanie([FromBody] int nroCompanie) =>
            await _companieService.GetNCompanies(nroCompanie);

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Companies>> GetCompanie(string id)
        {
            var Companies = await _companieService.GetCompanieById(id);

            if (Companies is null)
            {
                return NotFound();
            }

            return Companies;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanie(Companies newCompanies)
        {
            await _companieService.CreateCompanie(newCompanies);
            return CreatedAtAction(nameof(CreateCompanie), new { id = newCompanies.Id }, newCompanies);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCompanie(string id, Companies updatedCompanies)
        {
            var Companies = await _companieService.GetCompanieById(id);

            if (Companies is null)
            {
                return NotFound();
            }

            updatedCompanies.Id = Companies.Id;

            await _companieService.UpdateCompanie(id, updatedCompanies);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteCompanie(string id)
        {
            var Companies = await _companieService.GetCompanieById(id);

            if (Companies is null)
            {
                return NotFound();
            }

            await _companieService.DeleteCompanie(id);

            return NoContent();
        }
    }
}
