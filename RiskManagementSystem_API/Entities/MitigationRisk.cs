using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class MitigationRisk
    {

        public int Id { get; set; }

        public int MitigationId { get; set; }

        public int RiskId { get; set; }
    }
}
