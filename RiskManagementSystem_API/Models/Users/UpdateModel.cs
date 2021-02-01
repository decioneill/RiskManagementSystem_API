namespace RiskManagementSystem_API.Models.Users
{
  public class UpdateModel
    {
        public bool RiskManager { get; set; }
        public bool Admin { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}