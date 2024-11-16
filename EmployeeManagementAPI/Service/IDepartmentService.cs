using EmployeeManagementAPI.DTO;

namespace EmployeeManagementAPI.Service
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDTO> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(DepartmentDTO departmentDTO);
        Task UpdateDepartmentAsync(DepartmentDTO departmentDTO);
        Task DeleteDepartmentAsync(int id);
    }
}
