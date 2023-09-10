using webAPI_testTask.Models;

namespace webAPI_testTask.Repositories.Interfaces
{
    public interface IEmployeeRopository
    {
        public int AddNewEmployee(EmployeeCreate employee);
        public int DeleteEmployeeById(int id);
        public IEnumerable<Employee> GetAllEmployees();
        public IEnumerable<Employee> GetEmployeesByDepartmentId(int id);
        public IEnumerable<Employee> GetEmployeesByCompanyId(int id);
        public void EditEmployeeById(string update, int id);
        public Employee GetEmployeeById(int id);

    }
}
