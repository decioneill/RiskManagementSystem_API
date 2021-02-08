using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class RiskOwner
    {
        public Guid Id { get; set; }
        public Guid RiskId { get; set; }
        public Guid UserId { get; set; }
        public int Priority { get; set; }
    }
}
