using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementAPI.Domain;

namespace EmployeeManagementAPI.Repository
{
    

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = new List<Employee>();
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT * FROM Employees", connection);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                employees.Add(new Employee
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    DepartmentId = reader.GetInt32(2),
                    Salary = reader.GetDecimal(3),
                });
            }
            return employees;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            Employee employee = null;
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT * FROM Employees WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                employee = new Employee
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    DepartmentId = reader.GetInt32(2),
                    Salary = reader.GetDecimal(3),
                };
            }
            return employee;
        }

        public async Task AddAsync(Employee employee)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("INSERT INTO Employees (Name, DepartmentId, Salary) VALUES (@Name, @DepartmentId, @Salary)", connection);
            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
            command.Parameters.AddWithValue("@Salary", employee.Salary);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("UPDATE Employees SET Name = @Name, DepartmentId = @DepartmentId, Salary = @Salary WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
            command.Parameters.AddWithValue("@Salary", employee.Salary);
            command.Parameters.AddWithValue("@Id", employee.Id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("DELETE FROM Employees WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }

}
