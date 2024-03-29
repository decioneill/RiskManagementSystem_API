﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Models.Users
{
    public class TeamMemberModel
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = "";
        public bool TeamLeader { get; set; }
    }
}
