﻿namespace EmployeeManagementAPI.DTO
{
    public class SalaryDTO
    {
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
