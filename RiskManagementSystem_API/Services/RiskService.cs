using System;
using System.Collections.Generic;
using System.Linq;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Helpers;

namespace RiskManagementSystem_API.Services
{
    public interface IRiskService
    {
        IEnumerable<RiskProperty> GetRiskPropertiesForRisk(Guid riskId);
        IEnumerable<Risk> GetAll();
        Risk GetById(Guid id);
        IEnumerable<Risk> GetByUserId(Guid userId);
        Risk Create();
        void Update(Risk risk);
        void Delete(Guid id);
    }

    public class RiskService : IRiskService
    {
        private DataContext _context;

        public RiskService(DataContext context)
        {
            _context = context;
        }

        public Risk Create()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Risk> GetAll()
        {
            throw new NotImplementedException();
        }

        public Risk GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Risk> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RiskProperty> GetRiskPropertiesForRisk(Guid riskId)
        {
            IEnumerable<RiskProperty> properties = _context.RiskProperties.Where(x => x.RiskId.Equals(riskId));
            return properties;
        }

        public void Update(Risk risk)
        {
            throw new NotImplementedException();
        }
    }
}
