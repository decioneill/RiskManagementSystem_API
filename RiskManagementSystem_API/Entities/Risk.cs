using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class Risk
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }
    }
}
