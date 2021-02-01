namespace RiskManagementSystem_API.Models.Users
{
  public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        public bool RiskManager { get; set; }
    }
}