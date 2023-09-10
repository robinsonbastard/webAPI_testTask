using System.ComponentModel.DataAnnotations;

namespace webAPI_testTask.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public int CompanyId { get; set; }

        [Required]
        public Passport? Passport { get; set; }
        [Required]
        public Department? Department { get; set; }
    }
}
