using EmployeeManagementAPI.DTO;
using EmployeeManagementAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _service.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _service.GetDepartmentByIdAsync(id);
            if (department == null)
                return NotFound($"Department with ID {id} not found.");

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddDepartmentAsync(departmentDTO);
            return CreatedAtAction(nameof(GetById), new { id = departmentDTO.Id }, departmentDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentDTO departmentDTO)
        {
            if (id != departmentDTO.Id)
                return BadRequest("Department ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateDepartmentAsync(departmentDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }

}
