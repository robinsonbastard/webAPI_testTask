using System.ComponentModel.DataAnnotations;

namespace webAPI_testTask.Models
{
    public class Passport
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
