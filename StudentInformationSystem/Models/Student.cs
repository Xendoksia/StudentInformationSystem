using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace StudentInformationSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the student's identity number")]
        [StringLength(11)]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "Please enter the student's name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the student's surname")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter the student's gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter the student's birthdate")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Please enter the student's phone number")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter the student's email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Remote("ValidateEmail", "Student", HttpMethod = "Post", ErrorMessage = "This email already exists.")]
        public string Email { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter the student's student number")]
        [StringLength(9)]
        [Remote("ValidateStudentNumber", "Student", HttpMethod = "Post", ErrorMessage = "This student number already exists.")]
        public string StudentNumber { get; set; }

      
    }
}
