using System.ComponentModel.DataAnnotations;

namespace RiskManagementSystem_API.Models.Users
{
    public class AuthenticateModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}