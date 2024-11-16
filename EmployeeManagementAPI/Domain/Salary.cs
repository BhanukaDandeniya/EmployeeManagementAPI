namespace EmployeeManagementAPI.Domain
{
    public class Salary
    {
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
