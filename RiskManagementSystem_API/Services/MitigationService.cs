using System;
using System.Collections.Generic;
using System.Linq;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Helpers;

namespace RiskManagementSystem_API.Services
{
    public interface IMitigationService
    {
        IEnumerable<Mitigation> GetAll();
        Mitigation GetById(Guid id);
        IEnumerable<Mitigation> GetByRiskId(Guid riskId);
        Mitigation Create();
        void Update();
        void Delete(Guid id);
    }

    public class MitigationService : IMitigationService
    {
        private DataContext _context;

        public MitigationService(DataContext context)
        {
            _context = context;
        }

        public Mitigation Create()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mitigation> GetAll()
        {
            throw new NotImplementedException();
        }

        public Mitigation GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mitigation> GetByRiskId(Guid riskId)
        {
            IEnumerable<Guid> mitigationIds = _context.MitigationRisks.Where(x => x.RiskId.Equals(riskId)).Select(x => x.MitigationId);
            var list = from mitigation in _context.Mitigations
                       where mitigationIds.Contains(mitigation.Id)
                       select new Mitigation
                       {
                           Id = mitigation.Id,
                           CurrentStatus = mitigation.CurrentStatus,
                           Description = mitigation.Description,
                           ReviewDate = mitigation.ReviewDate,
                           Name = mitigation.Name
                       };
            List<Mitigation> mitigations = list.ToList<Mitigation>();
            return mitigations;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
