using EmployeeManagementAPI.DTO;
using EmployeeManagementAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalariesController : ControllerBase
    {
        private readonly ISalaryService _service;

        public SalariesController(ISalaryService service)
        {
            _service = service;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(int employeeId)
        {
            var salaries = await _service.GetSalariesByEmployeeIdAsync(employeeId);
            return Ok(salaries);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SalaryDTO salaryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddSalaryAsync(salaryDTO);
            return CreatedAtAction(nameof(GetByEmployeeId), new { employeeId = salaryDTO.EmployeeId }, salaryDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SalaryDTO salaryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateSalaryAsync(salaryDTO);
            return NoContent();
        }

        [HttpDelete("{employeeId}/{effectiveDate}")]
        public async Task<IActionResult> Delete(int employeeId, DateTime effectiveDate)
        {
            await _service.DeleteSalaryAsync(employeeId, effectiveDate);
            return NoContent();
        }
    }
}
