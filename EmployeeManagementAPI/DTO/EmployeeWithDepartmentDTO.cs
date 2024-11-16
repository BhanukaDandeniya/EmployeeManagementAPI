namespace EmployeeManagementAPI.DTO
{
    public class EmployeeWithDepartmentDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
