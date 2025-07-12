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
    public class DepartmentsController : ControllerBase
    {
        ApplicationDbContext context = new ApplicationDbContext();
        [HttpGet]
        public IActionResult Index()
        {
            var departments = context.Departments.Select(category => new DepartmentDto()
            {
                Name = category.Name,
                Description = category.Description,
            }).ToList();

            return Ok(departments);
        }

        [HttpPost]
        public IActionResult Create(DepartmentDto requestDto)
        {
            try
            {
                var departments = requestDto.Adapt<Department>();

                context.Departments.Add(departments);
                context.SaveChanges();
                return Ok("Department created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var departments = context.Departments.Find(id);
            if (departments == null)
            {
                return NotFound("Department not found");
            }
            context.Departments.Remove(departments);
            context.SaveChanges();
            return Ok("Department deleted successfully");
        }
        [HttpPatch]
        public IActionResult Update(DepartmentDto requestDto, int id)
        {
            if (requestDto == null)
            {
                return BadRequest("Department cannot be null");
            }
            var departments = context.Departments.Find(id);
            if (departments == null)
            {
                return NotFound("Department not found");
            }
            departments.Name = requestDto.Name;
            context.SaveChanges();
            return Ok("Department updated successfully");
        }
    }
}
