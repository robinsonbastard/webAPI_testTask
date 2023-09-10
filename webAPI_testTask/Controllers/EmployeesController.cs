using Microsoft.AspNetCore.Mvc;
using webAPI_testTask.Models;
using webAPI_testTask.Models.Employees;
using webAPI_testTask.Repositories.Interfaces;

namespace webAPI_testTask.Controllers
{
    //TODO убрать валидацию куда-нибудь, чтобы не дублировать
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRopository _employeeRepositoty;
        private readonly IDepartmentRepository _departmentRepositoty;

        public EmployeesController(IEmployeeRopository employeeRepositoty, IDepartmentRepository departmentRepositoty)
        {
            _employeeRepositoty = employeeRepositoty;
            _departmentRepositoty = departmentRepositoty;
        }

        [HttpPost]
        public ActionResult<int> AddNewEmployee(EmployeeCreate body)
        {
            try
            {
                var department = _departmentRepositoty.GetDepartmentById(body.DepartmentId);
                if (department == null)
                {
                    ModelState.AddModelError("Department",
                        $"There is no department with ID = {body.DepartmentId}");
                    return BadRequest(ModelState);
                }
                var id = _employeeRepositoty.AddNewEmployee(body);
                return Ok($"Employee create with ID = {id}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Employee with this passport details or phone already exist");
            }
        }   

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                var employee = _employeeRepositoty.GetEmployeeById(id);
                if (employee == null)
                    return BadRequest($"Employee with ID = {id} is not found");
                _employeeRepositoty.DeleteEmployeeById(id);
                return Ok($"Employee with ID = {id} successfully deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "SomeMagic");
            }

        }

        [HttpGet("company/{id}")]
        public ActionResult<IEnumerable<Employee>> GetEmloyeesByCompanyId(int id)
        {
            try
            {
                var result = _employeeRepositoty.GetEmployeesByCompanyId(id);
                if (result.Any())
                    return Ok(result);
                else 
                    return NotFound($"Employees with companyID = {id} not found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "SomeMagic");
            }
        }

        [HttpGet("department/{id}")]
        public ActionResult<IEnumerable<Employee>> GetEmployeeByDepartmentId(int id)
        {
            try
            {
                var result = _employeeRepositoty.GetEmployeesByDepartmentId(id);
                if (result.Any())
                    return Ok(result);
                else
                    return NotFound($"Employees with departmentID = {id} not found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "SomeMagic");
            }
        }

        [HttpPatch("{id}")]
        public ActionResult EditEmployee(EmployeeUpdate body, int id)
        {
            try
            {
                var employee = _employeeRepositoty.GetEmployeeById(id);
                if (employee == null)
                    return NotFound($"An employee with ID = {id} will not found");
                var update = body.GetType().GetProperties()
                    .Where(x => x.GetValue(body) != null)
                    .Select(x => $"{x.Name.ToLower()} = '{x.GetValue(body)}',")
                    .Aggregate("", (y, x) => y + x)
                    .TrimEnd(',', ' ');
                if (update == "") return NotFound("Empty request body");
                _employeeRepositoty.EditEmployeeById(update, id);
                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "SomeMagic");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployee()
        {
            try
            {
                var result = _employeeRepositoty.GetAllEmployees();
                if (result.Any())
                    return Ok(result);
                else
                    return NotFound($"Employees not found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "SomeMagic");
            }
        }
    }
}