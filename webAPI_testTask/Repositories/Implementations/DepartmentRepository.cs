using Dapper;
using webAPI_testTask.Models;
using webAPI_testTask.Repositories.Interfaces;

namespace webAPI_testTask.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private DataBaseConnection _context;
        public DepartmentRepository(DataBaseConnection context) 
        {
            _context = context;
        }
        public Department GetDepartmentById(int id)
        {
            var connection = _context.CreateConnection();
            var sql = $"SELECT * FROM departments WHERE departments.id = {id}";
            return connection.QuerySingleOrDefault<Department>(sql);
        }

    }
}
