using System;
using System.Collections.Generic;
using System.Linq;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Helpers;
using RiskManagementSystem_API.Models.Risks;

namespace RiskManagementSystem_API.Services
{
    public interface IRiskService
    {
        IEnumerable<RiskProperty> GetRiskPropertiesForRisk(Guid riskId);
        IEnumerable<Risk> GetAll();
        Risk GetRiskById(Guid id);
        IEnumerable<Risk> GetByUserId(Guid userId);
        IEnumerable<SimpleRisk> GetSimpleRisks(Guid projectId, Guid userId);
        void Create(Risk risk);
        void Update(Risk risk);
        void UpdateProperties(List<RiskProperty> riskProperties);
        void Delete(Guid id);
    }

    public class RiskService : IRiskService
    {
        private DataContext _context;

        public RiskService(DataContext context)
        {
            _context = context;
        }

        public void Create(Risk risk)
        {
            _context.Risks.Add(risk);
            List<RiskProperty> riskProperties = new List<RiskProperty>();
            foreach(RiskPropertyTypes type in (RiskPropertyTypes[]) Enum.GetValues(typeof(RiskPropertyTypes)))
            {
                RiskProperty prop = new RiskProperty()
                {
                    PropertyId = (int)type,
                    PropertyValue = "1",
                    RiskId = risk.Id
                };
                riskProperties.Add(prop);
            }
            _context.RiskProperties.AddRange(riskProperties);

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var risk = _context.Risks.Find(id);
            if (risk != null)
            {
                _context.Risks.Remove(risk);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Risk> GetAll()
        {
            throw new NotImplementedException();
        }

        public Risk GetRiskById(Guid id)
        {
            Risk risk = _context.Risks.FirstOrDefault(r => r.Id.Equals(id));
            return risk;
        }

        public IEnumerable<Risk> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<SimpleRisk> GetSimpleRisks(Guid projectId, Guid userId)
        {
            User user = _context.Users.SingleOrDefault(u => u.Id.Equals(userId));
            if (user != null)
            {
                IEnumerable<Guid> riskIds;
                if (!user.Admin)
                {
                    riskIds = _context.RiskOwners
                        .Where(risk => risk.UserId.Equals(userId))
                        .Select(risk => risk.RiskId); 
                }
                else
                {
                    riskIds = _context.RiskOwners.ToList().Select(r => r.RiskId);
                }
                var list = from risk in _context.Risks
                           where (risk.ProjectId.Equals(projectId) && !riskIds.Contains(risk.Id))
                           select new SimpleRisk
                           {
                               Id = risk.Id,
                               ProjectId = risk.ProjectId,
                               ShortDescription = risk.ShortDescription
                           };
                List<SimpleRisk> risks = list.ToList<SimpleRisk>();
                foreach(SimpleRisk sRisk in risks)
                {
                    sRisk.InherentRiskScore = GetRiskScore(sRisk, RiskScoreTypes.InherentRiskScore);
                    sRisk.ResidualRiskScore = GetRiskScore(sRisk, RiskScoreTypes.ResidualRiskScore);
                    sRisk.FutureRiskScore = GetRiskScore(sRisk, RiskScoreTypes.FutureRiskScore);
                }
                return risks;
            }
            return null;
        }

        private int GetRiskScore(SimpleRisk sRisk, RiskScoreTypes type)
        {
            string Likelihood = ""; 
            string Impact = "";
            switch (type)
            {
                case RiskScoreTypes.InherentRiskScore:
                    Likelihood = GetPropertyValue(sRisk.Id, RiskPropertyTypes.InherentLikelihood);
                    Impact = GetPropertyValue(sRisk.Id, RiskPropertyTypes.InherentImpact);
                    break;
                case RiskScoreTypes.ResidualRiskScore:
                    Likelihood = GetPropertyValue(sRisk.Id, RiskPropertyTypes.ResidualLikelihood);
                    Impact = GetPropertyValue(sRisk.Id, RiskPropertyTypes.ResidualImpact);
                    break;
                case RiskScoreTypes.FutureRiskScore:
                    Likelihood = GetPropertyValue(sRisk.Id, RiskPropertyTypes.FurtureLikelihood);
                    Impact = GetPropertyValue(sRisk.Id, RiskPropertyTypes.FutureImpact);
                    break;
            }
            int l = int.Parse(Likelihood);
            int i = int.Parse(Impact);
            return l * i;
        }

        private string GetPropertyValue(Guid id, RiskPropertyTypes type)
        {
            RiskProperty property = _context.RiskProperties.Where(r => r.RiskId.Equals(id) && r.PropertyId.Equals((int)type)).FirstOrDefault();
            if(property != null)
            {
                return property.PropertyValue;
            }
            return "0";
        }

        public IEnumerable<RiskProperty> GetRiskPropertiesForRisk(Guid riskId)
        {
            IEnumerable<RiskProperty> properties = _context.RiskProperties.Where(x => x.RiskId.Equals(riskId));
            return properties;
        }

        public void Update(Risk risk)
        {
            _context.Risks.Update(risk);
            _context.SaveChanges();
        }

        public void UpdateProperties(List<RiskProperty> riskProperties)
        {
            _context.RiskProperties.UpdateRange(riskProperties);
            _context.SaveChanges();
        }
    }
}
