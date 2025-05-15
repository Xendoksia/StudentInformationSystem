using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace StudentInformationSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(11)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(100)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select a role")]
        public string Role { get; set; }

        [Required]
        [Remote("ValidateIdentityNumber", "User", ErrorMessage = "This identity already exists.")]
        public string IdentityNumber { get; set; }
    }
}