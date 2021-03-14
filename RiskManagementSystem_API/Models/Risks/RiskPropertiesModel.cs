using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskManagementSystem_API.Models.Risks
{
    public class RiskPropertiesModel
    {
        public Guid Id { get; set; }
        public Guid RiskId { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyValue { get; set; }
    }

    public enum RiskPropertyTypes
    {
        InherentLikelihood,
        InherentImpact,
        ResidualLikelihood,
        ResidualImpact,
        FutureImpact,
        FurtureLikelihood
    }
}
