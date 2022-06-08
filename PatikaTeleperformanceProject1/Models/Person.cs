using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaTeleperformanceProject1.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }

    public enum Gender
    {
        Male,Female,Other
    }

    public class PersonDTO
    {
        public string PhoneNumber { get; set; }
    }
}
