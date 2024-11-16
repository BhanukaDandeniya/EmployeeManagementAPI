namespace EmployeeManagementAPI.DTO
{
    public class EmployeeWithSalaryDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentId { get; set; }
        public List<SalaryDTO> SalaryHistory { get; set; }
    }
}
