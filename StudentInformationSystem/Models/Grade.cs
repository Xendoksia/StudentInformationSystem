using System;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Models
{
    public class Grade
    {
        internal string LessonName;

        public int Id { get; set; }

        [Required(ErrorMessage = "Please select the student")]
        public string StudentNumber { get; set; } // Reference to Student.StudentNumber

        [Required(ErrorMessage = "Please select the lesson")]
        public string Code { get; set; } // Reference to Lesson.Code

        // Additional properties
        public int StudentId { get; set; } // Reference to Student.Id
        public int LessonId { get; set; } // Reference to Lesson.Id

        [Required(ErrorMessage = "Please enter the grade value")]
        public string GradeValue { get; set; }
        
    }
}