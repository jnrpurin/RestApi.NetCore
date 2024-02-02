using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Interface;
using WebApp.Domain.Models;

namespace WebApp.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await employeeService.GetEmployees());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            return Ok(await employeeService.GetEmployeeById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            return Ok(await employeeService.CreateEmployee(employee));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            return Ok(await employeeService.UpdateEmployee(employee));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            return Ok(await employeeService.DeleteEmployee(id));
        }

        [HttpPut("disableEmployee")]
        public async Task<IActionResult> DisableEmployee(int id)
        {
            return Ok(await employeeService.DisableEmployee(id));
        }
    }
}
