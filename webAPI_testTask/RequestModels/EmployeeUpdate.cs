namespace webAPI_testTask.Models.Employees
{
    public class EmployeeUpdate
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public int? CompanyId { get; set; }
        public string? PassportNumber { get; set; }
        public string? PassportType { get; set; }
        public int? DepartmentId { get; set; }
    }
}
