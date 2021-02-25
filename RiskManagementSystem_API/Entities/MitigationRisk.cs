using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class MitigationRisk
    {
        public Guid MitigationId { get; set; }

        public Guid RiskId { get; set; }
    }
}
