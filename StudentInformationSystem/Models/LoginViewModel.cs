using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string IdentityNumber { get; set; }
    }
}
