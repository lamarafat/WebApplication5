using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Model;
using WebApplication5.Model.DTO;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        ApplicationDbContext context = new ApplicationDbContext();
        [HttpGet]
        public IActionResult Index()
        {
            var employees = context.Employees.ToList();

            return Ok(employees);
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Employee cannot be null");
            }
            var employee = requestDto.Adapt<Employee>();
            context.Employees.Add(employee);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            context.Employees.Remove(employee);
            context.SaveChanges();
            return Ok("Employee deleted successfully");
        }
        [HttpPatch]
        public IActionResult Update(EmployeeDto requestDto, int id)
        {
            if (requestDto == null)
            {
                return BadRequest("Employee cannot be null");
            }
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            employee.Name = requestDto.Name;
            employee.Position = requestDto.Position;
            employee.Salary = requestDto.Salary;
            employee.DepartmentId = requestDto.DepartmentId;


            context.SaveChanges();
            return Ok("Employee updated successfully");
        }
    }
}
