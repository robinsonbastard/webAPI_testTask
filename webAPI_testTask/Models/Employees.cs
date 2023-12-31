﻿using System.ComponentModel.DataAnnotations;

namespace webAPI_testTask.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }

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
