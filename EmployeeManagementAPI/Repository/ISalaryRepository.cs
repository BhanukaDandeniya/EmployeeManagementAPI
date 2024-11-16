using EmployeeManagementAPI.Domain;

namespace EmployeeManagementAPI.Repository
{
    public interface ISalaryRepository
    {
        Task<IEnumerable<Salary>> GetSalariesByEmployeeIdAsync(int employeeId);
        Task AddSalaryAsync(Salary salary);
        Task UpdateSalaryAsync(Salary salary);
        Task DeleteSalaryAsync(int employeeId, DateTime effectiveDate);
    }
}
