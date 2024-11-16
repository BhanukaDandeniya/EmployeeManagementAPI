using EmployeeManagementAPI.Domain;
using EmployeeManagementAPI.DTO;

namespace EmployeeManagementAPI.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<EmployeeWithDepartmentDTO> GetEmployeeWithDepartmentAsync(int id);
        Task<EmployeeWithSalaryDTO> GetEmployeeWithSalaryHistoryAsync(int id);
    }
}
