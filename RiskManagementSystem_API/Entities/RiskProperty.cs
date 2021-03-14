using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class RiskProperty
    {
        public Guid RiskId { get; set; }
        public int PropertyId { get; set; }
        public string PropertyValue { get; set; }
    }
}
