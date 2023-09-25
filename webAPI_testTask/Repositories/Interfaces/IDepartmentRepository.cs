using Dapper;
using webAPI_testTask.CreateModels;
using webAPI_testTask.Models;
using webAPI_testTask.UpdateModels;

namespace webAPI_testTask.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        public Department GetDepartmentByDetails(Department department);
        public int CreateNewDepartment(DepartmentCreate department);
        public IEnumerable<Department> GetAllDepartments();
        public Department GetDepartmentById(int id);
        public bool CheckDepartmentExistence(Department department);
        public void EditDepartmentById(DynamicParameters parameters, string update);
    }
}
