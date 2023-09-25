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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRopository _employeeRepositoty;
        private readonly IDepartmentRepository _departmentRepositoty;

        public EmployeesController(
            IEmployeeRopository employeeRepositoty, 
            IDepartmentRepository departmentRepositoty)
        {
            _employeeRepositoty = employeeRepositoty;
            _departmentRepositoty = departmentRepositoty;
        }

        [HttpPost]
        public ActionResult<int> CreateEmployee(EmployeeCreate body)
        {
            try
            {
                if (!_departmentRepositoty.CheckDepartmentExistence(body.Department))
                    return BadRequest($"Department does not exist");
                var id = _employeeRepositoty.CreateNewEmployee(body);
                return Ok($"Employee create with ID = {id}");
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
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
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
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
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [HttpGet("department/{id}")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesByDepartmentId(int id)
        {
            try
            {
                if (_departmentRepositoty.GetDepartmentById(id) == null)
                    return BadRequest($"Department with ID = {id} does not exist");
                var result = _employeeRepositoty.GetEmployeesByDepartmentId(id);
                if (result.Any())
                    return Ok(result);
                else
                    return NotFound($"Employees with this department not found");
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [HttpPatch("{id}")]
        public ActionResult EditEmployee(EmployeeUpdate body, int id)
        {
            try
            {
                var employee = _employeeRepositoty.GetEmployeeById(id);
                if (employee == null)
                    return NotFound($"An employee with ID = {id} not found");
                if (body.DepartmentId != null)
                    if (_departmentRepositoty.GetDepartmentById((int)body.DepartmentId) == null)
                        return BadRequest($"Department with ID = {body.DepartmentId} is not exist");
                var parameters = new DynamicParameters();
                parameters.Add("Id", id);
                var updateEmployee = body.GetType().GetProperties()
                    .Where(x => x.GetValue(body) != null && x.Name != "Passport")
                    .Select(x => { parameters.Add(x.Name, x.GetValue(body)); return $"{x.Name.ToLower()} = @{x.Name},"; })
                    .Aggregate("", (y, x) => $"{y} {x}")
                    .TrimEnd(',', ' ');
                var updatePassport = body.Passport?.GetType().GetProperties()
                    .Where(x => x.GetValue(body.Passport) != null)
                    .Select(x => { parameters.Add(x.Name, x.GetValue(body.Passport)); return $"{x.Name.ToLower()} = @{x.Name},"; })
                    .Aggregate("", (y, x) => $"{y} {x}")
                    .TrimEnd(',', ' ');
                if (string.IsNullOrEmpty(updateEmployee) && string.IsNullOrEmpty(updatePassport))
                    return BadRequest("Empty request body");
                _employeeRepositoty.EditEmployeeById(parameters, updateEmployee, updatePassport);
                return NoContent();
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                var result = _employeeRepositoty.GetAllEmployees();
                if (result.Any())
                    return Ok(result);
                else
                    return NotFound($"Employees not found");
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }
    }
}