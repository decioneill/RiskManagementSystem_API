using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class RiskOwner
    {
        public Guid RiskId { get; set; }
        public Guid UserId { get; set; }
        public int Priority { get; set; }
    }
}
