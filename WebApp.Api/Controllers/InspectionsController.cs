using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Services;

namespace WebApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InspectionsController : ControllerBase
    {
        private readonly InspectionsService inspectionsService;
        public InspectionsController(InspectionsService inspectionsService)
        {
            this.inspectionsService = inspectionsService;
        }

        [HttpGet]
        public IActionResult GetInspections(string name) => Ok(inspectionsService.GetInspections(name));

        [HttpGet("{id:length(24)}")]
        public IActionResult GetAnInspection(string id)
        {
            var insp = inspectionsService.GetAnInspections(id);

            if (insp is null)
                return NotFound();
            
            return Ok(insp);
        }
    }
}
