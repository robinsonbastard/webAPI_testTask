namespace webAPI_testTask.UpdateModels
{
    public class EmployeeUpdate
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Phone { get; set; }

        public int? CompanyId { get; set; }
        public int? DepartmentId { get; set; }
        public PassportUpdate? Passport { get; set; }

    }
}
