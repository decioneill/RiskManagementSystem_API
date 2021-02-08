using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class RiskStatusHistory
    {
        public Guid Id { get; set; }
        public Guid RiskId { get; set; }
        public int Status { get; set; }
        public Guid StatusChangedBy { get; set; }
        public DateTime StatusChangeDate { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
