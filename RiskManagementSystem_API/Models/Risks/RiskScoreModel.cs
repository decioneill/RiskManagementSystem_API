using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Models.Risks
{
    public class RiskScoreModel
    {
        public int Impact { get; set; } = 1;

        public int Likelihood { get; set; } = 1;

        public int RiskScore 
        { 
            get
            {
                return Impact * Likelihood;
            }
        }

        public RiskScoreTypes ScoreType { get; set; }
    }
}
