﻿using System.ComponentModel.DataAnnotations;

namespace webAPI_testTask.Models
{
    public class Department
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
