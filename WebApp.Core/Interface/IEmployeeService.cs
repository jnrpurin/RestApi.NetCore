using WebApp.Domain.Models;
using WebApp.Domain.Request;

namespace WebApp.Core.Interface
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<List<Employee>>> GetEmployees();
        Task<ServiceResponse<Employee>> GetEmployeeById(int id);
        Task<ServiceResponse<List<Employee>>> CreateEmployee(Employee newEmployee);
        Task<ServiceResponse<Employee>> UpdateEmployee(Employee editEmployee);
        Task<ServiceResponse<List<Employee>>> DeleteEmployee(int id);
        Task<ServiceResponse<Employee>> DisableEmployee(int id);
    }
}
