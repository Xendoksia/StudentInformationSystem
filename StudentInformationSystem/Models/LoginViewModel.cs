using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Identity number is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string IdentityNumber { get; set; }
    }
}
