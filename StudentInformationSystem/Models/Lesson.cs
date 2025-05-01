using System;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the lesson's code")]
        [StringLength(8)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter the lesson's name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the lesson's semester")]
        [StringLength(1)]
        public string Semester { get; set; }

        public int Credit { get; set; }

        // Add other properties as needed
    }
}
