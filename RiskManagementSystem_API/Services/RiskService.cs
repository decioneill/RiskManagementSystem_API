using System;
using System.Collections.Generic;
using System.Linq;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Helpers;

namespace RiskManagementSystem_API.Services
{
    public interface IRiskService
    {
        IEnumerable<RiskProperty> GetRiskPropertiesForRisk(int Id);
        IEnumerable<Risk> GetAll();
        Risk GetById(int id);
        IEnumerable<Risk> GetByUserId(int userId);
        Risk Create();
        void Update(Risk risk);
        void Delete(int id);
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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Risk> GetAll()
        {
            throw new NotImplementedException();
        }

        public Risk GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Risk> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RiskProperty> GetRiskPropertiesForRisk(int riskId)
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
