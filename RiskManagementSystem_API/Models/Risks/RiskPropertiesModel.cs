using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Models.Risks
{
    public class RiskPropertiesModel
    {
        public int Id { get; set; }
        public int RiskId { get; set; }
        public int PropertyId { get; set; }
        public string PropertyValue { get; set; }
    }
}
