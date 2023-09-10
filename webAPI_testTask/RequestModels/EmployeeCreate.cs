using System.ComponentModel.DataAnnotations;

namespace webAPI_testTask.Models
{
    public class EmployeeCreate
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string? PassportNumber { get; set; }

        [Required]
        public string? PassportType { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
