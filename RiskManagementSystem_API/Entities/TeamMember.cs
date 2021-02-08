using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int UserId { get; set; }
        public bool TeamLeader { get; set; }
    }
}
