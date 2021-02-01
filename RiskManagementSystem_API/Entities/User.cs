using System;

namespace RiskManagementSystem_API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool RiskManager { get; set; }
        public bool Admin { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}