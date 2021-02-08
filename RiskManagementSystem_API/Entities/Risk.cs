using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class Risk
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }
    }
}
