namespace EmployeeManagementAPI.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public decimal Salary { get; set; }
    }
}
