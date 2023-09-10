using webAPI_testTask.Models;
using webAPI_testTask.Models.Employees;

namespace webAPI_testTask.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        public Department GetDepartmentById(int id);
    }
}
