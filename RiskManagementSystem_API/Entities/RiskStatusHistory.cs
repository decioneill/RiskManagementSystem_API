using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Entities
{
    public class RiskStatusHistory
    {
        public int Id { get; set; }
        public int RiskId { get; set; }
        public int Status { get; set; }
        public int StatusChangedBy { get; set; }
        public DateTime StatusChangeDate { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
