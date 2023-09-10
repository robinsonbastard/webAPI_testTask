using webAPI_testTask.Models;
using Dapper;
using webAPI_testTask.Repositories.Interfaces;

namespace webAPI_testTask.Repositories.Implementations
{
    //TODO убрать дублирование подключения к бд
    public class EmployeeRopository : IEmployeeRopository
    {
        private DataBaseConnection _context;

        public EmployeeRopository(DataBaseConnection context)
        {
            _context = context;
        }

        public int AddNewEmployee(EmployeeCreate e)
        {
            var connection = _context.CreateConnection();
            var sql =
                $"INSERT INTO employees (name, surname, phone, companyid, departmentid, passporttype, passportnumber) " +
                $"VALUES ('{e.Name}', '{e.Surname}', '{e.Phone}', '{e.CompanyId}', '{e.DepartmentId}', '{e.PassportType}', '{e.PassportNumber}')" +
                $"RETURNING id;";
            var id = connection.QuerySingleOrDefault<int>(sql);
            return id;
        }

        public int DeleteEmployeeById(int id)
        {
            var connection = _context.CreateConnection();
            var sql =
                $"DELETE FROM employees " +
                $"WHERE employees.id = {id}" +
                $"RETURNING id;";
            return connection.QueryFirstOrDefault<int>(sql);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var connection = _context.CreateConnection();
            var sql =
                $"SELECT e.id, e.name, e.surname, e.phone, e.companyid, e.departmentid, " +
                $"e.passporttype AS type, e.passportnumber AS number, " +
                $"d.id AS departmentid, d.name AS name, d.phone AS phone " +
                $"FROM employees AS e " +
                $"JOIN departments AS d ON d.id = e.departmentid ";
            return connection.Query<Employee, Passport, Department, Employee>
                (sql, (e, p, d) => { e.Passport = p; e.Department = d; return e; }, splitOn: "departmentid");
        }

        public IEnumerable<Employee> GetEmployeesByDepartmentId(int id)
        {
            var connection = _context.CreateConnection();
            var sql =
                $"SELECT e.id, e.name, e.surname, e.phone, e.companyid, e.departmentid, " +
                $"e.passporttype AS type, e.passportnumber AS number, " +
                $"d.id AS departmentid, d.name AS name, d.phone AS phone " +
                $"FROM employees AS e " +
                $"JOIN departments AS d ON d.id = e.departmentid " +
                $"WHERE e.departmentid = {id};";
            return connection.Query<Employee, Passport, Department, Employee>
                (sql, (e, p, d) => { e.Passport = p; e.Department = d; return e; }, splitOn: "departmentid");
        }

        public IEnumerable<Employee> GetEmployeesByCompanyId(int id)
        {
            var connection = _context.CreateConnection();
            var sql =
                $"SELECT e.id, e.name, e.surname, e.phone, e.companyid, e.departmentid, " +
                $"e.passporttype AS type, e.passportnumber AS number, " +
                $"d.id AS departmentid, d.name AS name, d.phone AS phone " +
                $"FROM employees AS e " +
                $"JOIN departments AS d ON d.id = e.departmentid " +
                $"WHERE e.companyid = {id};";
            return connection.Query<Employee, Passport, Department, Employee>
                (sql, (e, p, d) => { e.Passport = p; e.Department = d; return e; }, splitOn: "departmentid");
        }

        public void EditEmployeeById(string update, int id)
        {
            var connection = _context.CreateConnection();
            var sql =
                $"UPDATE employees " +
                $"SET {update} " +
                $"WHERE employees.id = {id};";
            connection.Query(sql);
        }

        public Employee GetEmployeeById(int id)
        {
            var connection = _context.CreateConnection();
            var sql =
                $"SELECT e.id, e.name, e.surname, e.phone, e.companyid, e.departmentid, " +
                $"e.passporttype AS type, e.passportnumber AS number, " +
                $"d.id AS departmentid, d.name AS name, d.phone AS phone " +
                $"FROM employees AS e " +
                $"JOIN departments AS d ON d.id = e.departmentid " +
                $"WHERE e.id = {id};";
            return connection.QueryFirstOrDefault<Employee>(sql);
        }
    }
}
