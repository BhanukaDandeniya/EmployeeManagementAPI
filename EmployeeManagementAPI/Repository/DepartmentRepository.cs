using EmployeeManagementAPI.Domain;
using System.Data.SqlClient;

namespace EmployeeManagementAPI.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var departments = new List<Department>();
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT * FROM Departments", connection);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                departments.Add(new Department
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            return departments;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            Department department = null;
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT * FROM Departments WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                department = new Department
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
            }
            return department;
        }

        public async Task AddAsync(Department department)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("INSERT INTO Departments (Name) VALUES (@Name)", connection);
            command.Parameters.AddWithValue("@Name", department.Name);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("UPDATE Departments SET Name = @Name WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Name", department.Name);
            command.Parameters.AddWithValue("@Id", department.Id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("DELETE FROM Departments WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
