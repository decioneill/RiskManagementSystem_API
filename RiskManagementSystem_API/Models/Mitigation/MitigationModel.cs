using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Models.Mitigation
{
    public class MitigationModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CurrentStatus { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
