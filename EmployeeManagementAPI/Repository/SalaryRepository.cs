using EmployeeManagementAPI.Domain;
using System.Data.SqlClient;

namespace EmployeeManagementAPI.Repository
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly string _connectionString;

        public SalaryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Salary>> GetSalariesByEmployeeIdAsync(int employeeId)
        {
            var salaries = new List<Salary>();
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT * FROM Salaries WHERE EmployeeId = @EmployeeId", connection);
            command.Parameters.AddWithValue("@EmployeeId", employeeId);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                salaries.Add(new Salary
                {
                    EmployeeId = reader.GetInt32(0),
                    Amount = reader.GetDecimal(1),
                    EffectiveDate = reader.GetDateTime(2)
                });
            }
            return salaries;
        }

        public async Task AddSalaryAsync(Salary salary)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("INSERT INTO Salaries (EmployeeId, Amount, EffectiveDate) VALUES (@EmployeeId, @Amount, @EffectiveDate)", connection);
            command.Parameters.AddWithValue("@EmployeeId", salary.EmployeeId);
            command.Parameters.AddWithValue("@Amount", salary.Amount);
            command.Parameters.AddWithValue("@EffectiveDate", salary.EffectiveDate);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateSalaryAsync(Salary salary)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("UPDATE Salaries SET Amount = @Amount WHERE EmployeeId = @EmployeeId AND EffectiveDate = @EffectiveDate", connection);
            command.Parameters.AddWithValue("@EmployeeId", salary.EmployeeId);
            command.Parameters.AddWithValue("@Amount", salary.Amount);
            command.Parameters.AddWithValue("@EffectiveDate", salary.EffectiveDate);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteSalaryAsync(int employeeId, DateTime effectiveDate)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("DELETE FROM Salaries WHERE EmployeeId = @EmployeeId AND EffectiveDate = @EffectiveDate", connection);
            command.Parameters.AddWithValue("@EmployeeId", employeeId);
            command.Parameters.AddWithValue("@EffectiveDate", effectiveDate);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
