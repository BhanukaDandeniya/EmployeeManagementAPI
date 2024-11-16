using EmployeeManagementAPI.Domain;
using EmployeeManagementAPI.DTO;
using EmployeeManagementAPI.Repository;
namespace EmployeeManagementAPI.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
        {
            try
            {
                var departments = await _repository.GetAllAsync();
                return departments.Select(d => new DepartmentDTO
                {
                    Id = d.Id,
                    Name = d.Name
                });
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception("An error occurred while fetching all departments.", ex);
            }
        }

        public async Task<DepartmentDTO> GetDepartmentByIdAsync(int id)
        {
            try
            {
                var department = await _repository.GetByIdAsync(id);
                if (department == null)
                    return null;

                return new DepartmentDTO
                {
                    Id = department.Id,
                    Name = department.Name
                };
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception($"An error occurred while fetching department with ID {id}.", ex);
            }
        }

        public async Task AddDepartmentAsync(DepartmentDTO departmentDTO)
        {
            try
            {
                var department = new Department
                {
                    Name = departmentDTO.Name
                };
                await _repository.AddAsync(department);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception("An error occurred while adding the new department.", ex);
            }
        }

        public async Task UpdateDepartmentAsync(DepartmentDTO departmentDTO)
        {
            try
            {
                var existingDepartment = await _repository.GetByIdAsync(departmentDTO.Id);
                if (existingDepartment == null)
                    throw new KeyNotFoundException($"Department with ID {departmentDTO.Id} not found.");

                existingDepartment.Name = departmentDTO.Name;

                await _repository.UpdateAsync(existingDepartment);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception($"An error occurred while updating department with ID {departmentDTO.Id}.", ex);
            }
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            try
            {
                var existingDepartment = await _repository.GetByIdAsync(id);
                if (existingDepartment == null)
                    throw new KeyNotFoundException($"Department with ID {id} not found.");

                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new Exception($"An error occurred while deleting department with ID {id}.", ex);
            }
        }
    }
}
