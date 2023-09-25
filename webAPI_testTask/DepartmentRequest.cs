using System.ComponentModel.DataAnnotations;

namespace webAPI_testTask.RequestModels
{
    public class DepartmentRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
