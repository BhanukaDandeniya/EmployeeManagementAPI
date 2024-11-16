using EmployeeManagementAPI.Domain;
using EmployeeManagementAPI.DTO;
using EmployeeManagementAPI.Repository;
namespace EmployeeManagementAPI.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // Get all employees
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _repository.GetAllAsync();
                return employees.Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    DepartmentId = e.DepartmentId,
                    Salary = e.Salary
                });
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception("An error occurred while fetching all employees.", ex);
            }
        }

        // Get employee by ID
        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _repository.GetByIdAsync(id);
                if (employee == null)
                    return null;

                return new EmployeeDTO
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    DepartmentId = employee.DepartmentId,
                    Salary = employee.Salary
                };
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception($"An error occurred while fetching employee with ID {id}.", ex);
            }
        }

        // Add a new employee
        public async Task AddEmployeeAsync(EmployeeDTO employeeDTO)
        {
            try
            {
                var employee = new Employee
                {
                    Name = employeeDTO.Name,
                    DepartmentId = employeeDTO.DepartmentId,
                    Salary = employeeDTO.Salary
                };
                await _repository.AddAsync(employee);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception("An error occurred while adding the new employee.", ex);
            }
        }

        // Update an existing employee
        public async Task UpdateEmployeeAsync(EmployeeDTO employeeDTO)
        {
            try
            {
                var existingEmployee = await _repository.GetByIdAsync(employeeDTO.Id);
                if (existingEmployee == null)
                    throw new KeyNotFoundException($"Employee with ID {employeeDTO.Id} not found.");

                existingEmployee.Name = employeeDTO.Name;
                existingEmployee.DepartmentId = employeeDTO.DepartmentId;
                existingEmployee.Salary = employeeDTO.Salary;

                await _repository.UpdateAsync(existingEmployee);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception($"An error occurred while updating employee with ID {employeeDTO.Id}.", ex);
            }
        }

        // Delete an employee
        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                var existingEmployee = await _repository.GetByIdAsync(id);
                if (existingEmployee == null)
                    throw new KeyNotFoundException($"Employee with ID {id} not found.");

                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception($"An error occurred while deleting employee with ID {id}.", ex);
            }
        }

        public async Task<EmployeeWithDepartmentDTO> GetEmployeeWithDepartmentAsync(int id)
        {
            try
            {
                return await _repository.GetEmployeeWithDepartmentAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching employee with department details for ID {id}.", ex);
            }
        }

        public async Task<EmployeeWithSalaryDTO> GetEmployeeWithSalaryHistoryAsync(int id)
        {
            try
            {
                return await _repository.GetEmployeeWithSalaryHistoryAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching employee with salary history for ID {id}.", ex);
            }
        }
    }
}
