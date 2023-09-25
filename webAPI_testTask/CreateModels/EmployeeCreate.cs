using System.ComponentModel.DataAnnotations;
using webAPI_testTask.Models;

namespace webAPI_testTask.CreateModels
{
    public class EmployeeCreate
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public Passport Passport { get; set; }

        [Required]
        public Department Department { get; set; }
    }
}
