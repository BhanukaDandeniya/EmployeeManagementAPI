using EmployeeManagementAPI.DTO;

namespace EmployeeManagementAPI.Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(EmployeeDTO employeeDTO);
        Task UpdateEmployeeAsync(EmployeeDTO employeeDTO);
        Task DeleteEmployeeAsync(int id);

        Task<EmployeeWithDepartmentDTO> GetEmployeeWithDepartmentAsync(int id);
        Task<EmployeeWithSalaryDTO> GetEmployeeWithSalaryHistoryAsync(int id);
    }
}
