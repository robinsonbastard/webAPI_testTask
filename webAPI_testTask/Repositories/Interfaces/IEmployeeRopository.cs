using Dapper;
using webAPI_testTask.CreateModels;
using webAPI_testTask.Models;

namespace webAPI_testTask.Repositories.Interfaces
{
    public interface IEmployeeRopository
    {
        public int CreateNewEmployee(EmployeeCreate employee);
        public int DeleteEmployeeById(int id);
        public IEnumerable<Employee> GetAllEmployees();
        public IEnumerable<Employee> GetEmployeesByDepartmentId(int id);
        public IEnumerable<Employee> GetEmployeesByCompanyId(int id);
        public void EditEmployeeById(DynamicParameters parameters, string? emp, string? pass);
        public Employee GetEmployeeById(int id);

    }
}
