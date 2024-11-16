using EmployeeManagementAPI.DTO;

namespace EmployeeManagementAPI.Service
{
    public interface ISalaryService
    {
        Task<IEnumerable<SalaryDTO>> GetSalariesByEmployeeIdAsync(int employeeId);
        Task AddSalaryAsync(SalaryDTO salaryDTO);
        Task UpdateSalaryAsync(SalaryDTO salaryDTO);
        Task DeleteSalaryAsync(int employeeId, DateTime effectiveDate);
    }
}
