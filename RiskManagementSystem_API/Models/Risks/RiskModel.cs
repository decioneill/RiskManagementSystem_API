using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Models.Risks
{
    public class RiskModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }
    }
}
