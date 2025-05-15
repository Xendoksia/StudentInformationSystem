using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the teacher's identity number")]
        [StringLength(11)]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "Please enter the student's name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the student's surname")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [Remote("ValidateEmail", "Teacher", HttpMethod = "Post", ErrorMessage = "This email already exists.")]
        public string Email { get; set; }


        [StringLength(100)]
        public string Office { get; set; }

     
    }
}
