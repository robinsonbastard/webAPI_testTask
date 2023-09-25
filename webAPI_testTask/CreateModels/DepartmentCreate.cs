using System.ComponentModel.DataAnnotations;

namespace webAPI_testTask.CreateModels
{
    public class DepartmentCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
