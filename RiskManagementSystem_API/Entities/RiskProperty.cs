using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class RiskProperty
    {
        public Guid Id { get; set; }
        public Guid RiskId { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyValue { get; set; }
    }
}
