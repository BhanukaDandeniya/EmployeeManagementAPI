using EmployeeManagementAPI.DTO;
using EmployeeManagementAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        // Get all employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // Get employee by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound($"Employee with ID {id} not found.");

            return Ok(employee);
        }

        // Add a new employee
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddEmployeeAsync(employeeDTO);
            return CreatedAtAction(nameof(GetById), new { id = employeeDTO.Id }, employeeDTO);
        }

        // Update an existing employee
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.Id)
                return BadRequest("Employee ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEmployee = await _service.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
                return NotFound($"Employee with ID {id} not found.");

            await _service.UpdateEmployeeAsync(employeeDTO);
            return NoContent();
        }

        // Delete an employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingEmployee = await _service.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
                return NotFound($"Employee with ID {id} not found.");

            await _service.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }

}
