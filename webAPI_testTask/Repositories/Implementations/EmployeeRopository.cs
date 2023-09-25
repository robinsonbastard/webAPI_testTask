using webAPI_testTask.Models;
using Dapper;
using webAPI_testTask.Repositories.Interfaces;
using webAPI_testTask.CreateModels;

namespace webAPI_testTask.Repositories.Implementations
{
    public class EmployeeRopository : IEmployeeRopository
    {
        private DataBaseConnection _context;

        public EmployeeRopository(DataBaseConnection context)
        {
            _context = context;
        }

        public int CreateNewEmployee(EmployeeCreate e)
        {
            var connection = _context.CreateConnection();
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                var sql = $"INSERT INTO employees (name, surname, phone, companyid, departmentid) " +
                            $"VALUES (@Name, @Surname, @Phone, @CompanyId, @DepartmentId) RETURNING id;";
                var parameters = new { Name = e.Name, Surname = e.Surname, Phone = e.Phone, CompanyId = e.CompanyId, DepartmentId = e.Department.Id };
                var id = connection.QuerySingleOrDefault<int>(sql, parameters);
                connection.Execute("INSERT INTO passports (type, number, employeeid) VALUES (@Type, @Number, @EmployeeId)", 
                    new { Type = e.Passport.Type, Number = e.Passport.Number, EmployeeId = id }, transaction);
                transaction.Commit();
                connection.Close();
                return id;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public int DeleteEmployeeById(int id)
        {
            var connection = _context.CreateConnection();
            connection.Open();
            var sql =$"DELETE FROM employees " +
                $"WHERE employees.id = @Id RETURNING id;";
            var result = connection.QueryFirstOrDefault<int>(sql, new { Id = id });
            connection.Close();
            return result;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var connection = _context.CreateConnection();
            connection.Open();
            var sql = $"SELECT e.id, e.name, e.surname, e.phone, e.companyid, " +
                $"p.type, p.number, " +
                $"d.id, d.name, d.phone " +
                $"FROM employees AS e " +
                $"JOIN departments AS d ON d.id = e.departmentid " + 
                $"JOIN passports AS p ON p.employeeid = e.id ";
            var result =  connection.Query<Employee, Passport, Department, Employee>
                (sql, (e, p, d) => { e.Passport = p; e.Department = d; return e;}, splitOn: "type,id");
            connection.Close();
            return result;
        }

        public IEnumerable<Employee> GetEmployeesByDepartmentId(int id)
        {
            var connection = _context.CreateConnection();
            var sql =
                $"SELECT e.id, e.name, e.surname, e.phone, e.companyid, " +
                $"p.type, p.number, " +
                $"d.id, d.name, d.phone " +
                $"FROM employees AS e " +
                $"JOIN departments AS d ON d.id = e.departmentid " +
                $"JOIN passports AS p ON p.employeeid = e.id " +
                $"WHERE e.departmentid = @Id;";
            var result = connection.Query<Employee, Passport, Department, Employee>
                (sql, (e, p, d) => { e.Passport = p; e.Department = d; return e; }, new { Id = id }, splitOn: "type,id");
            connection.Close();
            return result;
        }

        public IEnumerable<Employee> GetEmployeesByCompanyId(int id)
        {
            var connection = _context.CreateConnection();
            connection.Open();
            var sql = $"SELECT e.id, e.name, e.surname, e.phone, e.companyid, " +
                $"p.type, p.number, " +
                $"d.id, d.name, d.phone " +
                $"FROM employees AS e " +
                $"JOIN departments AS d ON d.id = e.departmentid " +
                $"JOIN passports AS p ON p.employeeid = e.id " +
                $"WHERE e.companyid = @companyId;";
            var result = connection.Query<Employee, Passport, Department, Employee>
                (sql, (e, p, d) => { e.Passport = p; e.Department = d; return e; }, new { companyId = id }, splitOn: "type,id");
            connection.Close();
            return result;
        }

        public void EditEmployeeById(DynamicParameters parameters, string? emp, string? pass)
        {
            var connection = _context.CreateConnection();
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                if (!string.IsNullOrEmpty(emp))
                    connection.Execute($"UPDATE employees SET {emp} WHERE employees.id = @Id;", parameters, transaction);
                if (!string.IsNullOrEmpty(pass))
                    connection.Execute($"UPDATE passports SET {pass} WHERE employeeid = @Id;", parameters, transaction);
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            connection.Close();
        }

        public Employee GetEmployeeById(int id)
        {
            var connection = _context.CreateConnection();
            var sql = $"SELECT e.id, e.name, e.surname, e.phone, e.companyid, " +
                $"p.type, p.number, " +
                $"d.id, d.name, d.phone " +
                $"FROM employees AS e " +
                $"JOIN departments AS d ON d.id = e.departmentid " +
                $"JOIN passports AS p ON p.employeeid = e.id " +
                $"WHERE e.id = @Id;";
            var result = connection.Query<Employee, Passport, Department, Employee>
                (sql, (e, p, d) => { e.Passport = p; e.Department = d; return e; }, new { Id = id }, splitOn: "type,id").FirstOrDefault();
            connection.Close();
            return result;
        }
    }
}
