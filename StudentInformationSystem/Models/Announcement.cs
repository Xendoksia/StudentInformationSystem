using System;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Models
{
    public class Announcement
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the announcement's title")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the announcement's content")]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        // Add other properties as needed
    }
}