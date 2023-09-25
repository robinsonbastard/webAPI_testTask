using Dapper;
using Microsoft.AspNetCore.Mvc;
using webAPI_testTask.CreateModels;
using webAPI_testTask.Models;
using webAPI_testTask.Repositories.Interfaces;
using webAPI_testTask.UpdateModels;

namespace webAPI_testTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepositoty;

        public DepartmentsController(IDepartmentRepository departmentRepositoty)
        {
            _departmentRepositoty = departmentRepositoty;
        }

        [HttpPost]
        public ActionResult<int> CreateDepartment(DepartmentCreate body)
        {
            try
            {
                var id = _departmentRepositoty.CreateNewDepartment(body);
                return Ok($"Department create with ID = {id}");
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetAllDepartments()
        {
            try
            {
                var result = _departmentRepositoty.GetAllDepartments();
                if (result.Any())
                    return Ok(result);
                else
                    return NotFound($"Departments not found");
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [HttpPatch("{id}")]
        public ActionResult EditDepartment(DepartmentUpdate body, int id)
        {
            try
            {
                var employee = _departmentRepositoty.GetDepartmentById(id);
                if (employee.Equals(null))
                    return NotFound($"An department with ID = {id} will not found");
                var parameters = new DynamicParameters();
                parameters.Add("Id", id);
                var update = body.GetType().GetProperties()
                    .Where(x => x.GetValue(body) != null)
                    .Select(x =>
                    {
                        parameters.Add(x.Name, x.GetValue(body));
                        return $"{x.Name.ToLower()} = @{x.Name},";
                    })
                    .Aggregate("", (y, x) => $"{y} {x}")
                    .TrimEnd(',', ' ');
                if (string.IsNullOrEmpty(update))
                    return BadRequest("Empty request body");
                _departmentRepositoty.EditDepartmentById(parameters, update);
                return NoContent();
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

    }
}
