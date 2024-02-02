using Microsoft.EntityFrameworkCore;
using WebApp.Core.Interface;
using WebApp.Domain.Models;
using WebApp.Domain.Request;
using WebApp.Infra.Context;

namespace WebApp.Core.Services
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;
        public EmployeeService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<ServiceResponse<List<Employee>>> CreateEmployee(Employee newEmployee)
        {
            ServiceResponse<List<Employee>> serviceResponse = new();
            try
            {
                if (newEmployee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    return serviceResponse;
                }

                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Employees.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Employee>>> DeleteEmployee(int id)
        {
            ServiceResponse<List<Employee>> serviceResponse = new();
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Employee not found!";
                    return serviceResponse;
                }
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Employee>> DisableEmployee(int id)
        {
            ServiceResponse<Employee> serviceResponse = new();
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    return serviceResponse;
                }

                employee.IsActive = false;
                employee.ChangedDate = DateTime.Now.ToLocalTime();
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = employee;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Employee>> GetEmployeeById(int id)
        {
            ServiceResponse<Employee> serviceResponse = new();
            try
            {
                serviceResponse.Data = await _context.Employees.FirstOrDefaultAsync(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Employee>>> GetEmployees()
        {
            ServiceResponse<List<Employee>> serviceResponse = new();
            try
            {
                serviceResponse.Data = await _context.Employees.ToListAsync();
                if (!serviceResponse.Data.Any())
                    serviceResponse.Message = "No data found!";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Employee>> UpdateEmployee(Employee editEmployee)
        {
            ServiceResponse<Employee> serviceResponse = new();
            try
            {
                var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(editEmployee.Id));
                if (employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Employee not found!";
                    return serviceResponse;
                }
                
                editEmployee.ChangedDate = DateTime.Now.ToLocalTime();
                _context.Employees.Update(editEmployee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = editEmployee;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
