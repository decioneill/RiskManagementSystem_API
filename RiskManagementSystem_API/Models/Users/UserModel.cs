using System;

namespace RiskManagementSystem_API.Models.Users
{
  public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool Admin { get; set; }
        public bool RiskManager { get; set; }
    }
}