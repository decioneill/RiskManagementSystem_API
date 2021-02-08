using System.ComponentModel.DataAnnotations;

namespace RiskManagementSystem_API.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public bool RiskManager { get; set; } = false;

        [Required]
        public bool Admin { get; set; } = false;

        [Required]
        public string Password { get; set; }
    }
}