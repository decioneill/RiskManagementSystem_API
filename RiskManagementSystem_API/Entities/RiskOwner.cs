using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class RiskOwner
    {
        public int Id { get; set; }
        public int RiskId { get; set; }
        public int UserId { get; set; }
        public int Priority { get; set; }
    }
}
