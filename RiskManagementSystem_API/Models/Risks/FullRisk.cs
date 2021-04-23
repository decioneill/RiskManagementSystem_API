using RiskManagementSystem_API.Entities;
using System;
namespace RiskManagementSystem_API.Models.Risks
{
    public class FullRisk
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public int InherentImpact { get; set; }

        public int InherentLikelihood { get; set; }

        public int InherentRiskScore { get; set; }

        public int ResidualImpact { get; set; }

        public int ResidualLikelihood { get; set; }

        public int ResidualRiskScore { get; set; }

        public int FutureImpact { get; set; }

        public int FutureLikelihood { get; set; }

        public int FutureRiskScore { get; set; }

        public Risk GetRisk()
        {
            Risk risk = new Risk()
            {
                Id = this.Id,
                Description = this.Description,
                ProjectId = this.ProjectId,
                ShortDescription = this.ShortDescription
            };
            return risk;
        }

        public RiskScoreModel GetRiskScore(RiskScoreTypes type)
        {
            RiskScoreModel riskScore = new RiskScoreModel() { ScoreType = type };
            switch (type)
            {
                case RiskScoreTypes.InherentRiskScore:
                    riskScore.Impact = InherentImpact;
                    riskScore.Likelihood = InherentLikelihood;
                    break;
                case RiskScoreTypes.ResidualRiskScore:
                    riskScore.Impact = ResidualImpact;
                    riskScore.Likelihood = ResidualLikelihood;
                    break;
                case RiskScoreTypes.FutureRiskScore:
                    riskScore.Impact = FutureImpact;
                    riskScore.Likelihood = FutureLikelihood;
                    break;
            }
            return riskScore;
        }
    }
}
