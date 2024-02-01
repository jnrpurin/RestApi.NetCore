using WebApp.Core.Interface;

namespace WebApp.Core.Services
{
    internal class EmployeeService : IEmployeeService
    {

        /* Sample Put
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
         */
    }
}
