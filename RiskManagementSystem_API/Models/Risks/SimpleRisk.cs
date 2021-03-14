using System;

namespace RiskManagementSystem_API.Models.Risks
{
    public class SimpleRisk
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public string ShortDescription { get; set; }

        public int InherentRiskScore { get; set; }

        public int ResidualRiskScore { get; set; }
    }
}