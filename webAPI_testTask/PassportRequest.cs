using System.ComponentModel.DataAnnotations;

namespace webAPI_testTask.RequestModels
{
    public class PassportRequest
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Number { get; set; }
    }
}
