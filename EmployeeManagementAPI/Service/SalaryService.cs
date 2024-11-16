using EmployeeManagementAPI.Domain;
using EmployeeManagementAPI.DTO;
using EmployeeManagementAPI.Repository;
namespace EmployeeManagementAPI.Service
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _repository;
        public SalaryService(ISalaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SalaryDTO>> GetSalariesByEmployeeIdAsync(int employeeId)
        {
            try
            {
                var salaries = await _repository.GetSalariesByEmployeeIdAsync(employeeId);
                return salaries.Select(s => new SalaryDTO
                {
                    EmployeeId = s.EmployeeId,
                    Amount = s.Amount,
                    EffectiveDate = s.EffectiveDate
                });
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception($"An error occurred while fetching salaries for employee with ID {employeeId}.", ex);
            }
        }

        public async Task AddSalaryAsync(SalaryDTO salaryDTO)
        {
            try
            {
                var salary = new Salary
                {
                    EmployeeId = salaryDTO.EmployeeId,
                    Amount = salaryDTO.Amount,
                    EffectiveDate = salaryDTO.EffectiveDate
                };
                await _repository.AddSalaryAsync(salary);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception("An error occurred while adding the new salary.", ex);
            }
        }

        public async Task UpdateSalaryAsync(SalaryDTO salaryDTO)
        {
            try
            {
                var salary = new Salary
                {
                    EmployeeId = salaryDTO.EmployeeId,
                    Amount = salaryDTO.Amount,
                    EffectiveDate = salaryDTO.EffectiveDate
                };
                await _repository.UpdateSalaryAsync(salary);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception("An error occurred while updating the salary.", ex);
            }
        }

        public async Task DeleteSalaryAsync(int employeeId, DateTime effectiveDate)
        {
            try
            {
                await _repository.DeleteSalaryAsync(employeeId, effectiveDate);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception($"An error occurred while deleting salary for employee with ID {employeeId}.", ex);
            }
        }
    }
}
