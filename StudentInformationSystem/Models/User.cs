using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a username")]
        [StringLength(11)]
        public string Username { get; set; } // Identity number is used as username

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(100)]
        public string Password { get; set; } // Will be taken from database

        [Required(ErrorMessage = "Please select a role")]
        public string Role { get; set; }

        public string IdentityNumber { get; set; } // IdentityNumber alanı eklendi


        // Add other properties as needed
    }
}