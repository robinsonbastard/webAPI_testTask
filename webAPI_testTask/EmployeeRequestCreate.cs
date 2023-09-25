using System.ComponentModel.DataAnnotations;
using webAPI_testTask.Models;
using webAPI_testTask.RequestModels;

namespace webAPI_testTask
{
    public class EmployeeRequestCreate
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
        public Department Department { get; set; }

        [Required]
        public PassportRequest Passport { get; set; }
    }
}
