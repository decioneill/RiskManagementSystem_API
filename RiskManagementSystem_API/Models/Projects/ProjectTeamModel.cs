using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Models.Projects
{
    public class ProjectTeamModel
    {
        public ProjectTeamModel(Project project)
        {
            this.Id = project.Id;
            this.Name = project.Name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public TeamMemberModel[] Team { get; set; }
    }
}
