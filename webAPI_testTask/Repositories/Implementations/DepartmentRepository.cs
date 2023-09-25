using Dapper;
using webAPI_testTask.CreateModels;
using webAPI_testTask.Models;
using webAPI_testTask.Repositories.Interfaces;
using webAPI_testTask.UpdateModels;

namespace webAPI_testTask.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private DataBaseConnection _context;

        public DepartmentRepository(DataBaseConnection context) 
        {
            _context = context;
        }
        
        public Department GetDepartmentByDetails(Department department)
        {
            var connection = _context.CreateConnection();
            connection.Open();
            var sql = $"SELECT * FROM departments " +
                $"WHERE departments.id = @Id AND departments.name = @Name " +
                $"AND departments.phone = @Phone;";
            var result = connection.QuerySingleOrDefault<Department>(sql, department);
            connection.Close();
            return result;  
        }

        public int CreateNewDepartment(DepartmentCreate department)
        {
            var connection = _context.CreateConnection();
            connection.Open();
            var sql = $"INSERT INTO departments (name, phone) VALUES (@Name, @Phone) RETURNING id;";
            var result = connection.QuerySingleOrDefault<int>(sql, new { Name = department.Name, Phone = department.Phone });
            connection.Close();
            return result;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            var connection = _context.CreateConnection();
            connection.Open();
            var sql = $"SELECT d.id, d.name, d.phone FROM departments AS d;";
            var result = connection.Query<Department>(sql);
            connection.Close();
            return result;
        }

        public Department GetDepartmentById(int id)
        {
            var connection = _context.CreateConnection();
            connection.Open();
            var sql = $"SELECT d.id, d.name, d.phone FROM departments AS d WHERE d.id = @Id;";
            var result = connection.QuerySingleOrDefault<Department>(sql, new { Id = id });
            connection.Close();
            return result;
        }

        public bool CheckDepartmentExistence(Department department)
        {
            return GetDepartmentById(department.Id).Equals(department);
        }

        public void EditDepartmentById(DynamicParameters parameters, string? update)
        {
            var connection = _context.CreateConnection();
            connection.Open();
            var sql = $"UPDATE departments SET {update} WHERE departments.id = @Id;";
            connection.Execute(sql, parameters);
            connection.Close();
        }
    }
}
