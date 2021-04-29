using System;
using System.Collections.Generic;
using System.Linq;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Helpers;

namespace RiskManagementSystem_API.Services
{
    public interface IMitigationService
    {
        Mitigation GetMitigationById(Guid id);
        IEnumerable<Mitigation> GetMitigationsByRiskId(Guid riskId);
        void Create(Mitigation mitigation, MitigationRisk mitigationRisk);
        void Update(Mitigation mitigation);
        void DeleteFromRisk(Guid mitigationId, Guid riskId);
    }

    public class MitigationService : IMitigationService
    {
        private DataContext _context;

        public MitigationService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// creates new mitigation
        /// </summary>
        /// <param name="mitigation"></param>
        /// <param name="mr"></param>
        public void Create(Mitigation mitigation, MitigationRisk mr)
        {
            _context.Mitigations.Add(mitigation);
            _context.MitigationRisks.Add(mr);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes MitigationRisk and also Mitigation if no other MitigationRisk with MitigationId
        /// </summary>
        /// <param name="mitigationId"></param>
        /// <param name="riskId"></param>
        public void DeleteFromRisk(Guid mitigationId, Guid riskId)
        {
            IEnumerable<MitigationRisk> mitigationsOnRisk = _context.MitigationRisks.Where(x => x.RiskId.Equals(riskId));
            if (mitigationsOnRisk.Any())
            {
                MitigationRisk mr = mitigationsOnRisk.FirstOrDefault(x => x.MitigationId.Equals(mitigationId));
                if(mr is not null)
                {
                    _context.MitigationRisks.Remove(mr);
                    // If was only connection to Mitigation, delete Mitigation as well
                    if(mitigationsOnRisk.Count() == 1)
                    {
                        Mitigation mitigation = _context.Mitigations.Find(mitigationId);
                        _context.Mitigations.Remove(mitigation);
                    }
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets mitgation of id
        /// </summary>
        /// <param name="mitigationid"></param>
        /// <returns></returns>
        public Mitigation GetMitigationById(Guid mitigationid)
        {
            Mitigation mitigation = _context.Mitigations.FirstOrDefault(m => m.Id.Equals(mitigationid));
            return mitigation;
        }

        /// <summary>
        /// Gets list of mitigations on a risk
        /// </summary>
        /// <param name="riskId"></param>
        /// <returns></returns>
        public IEnumerable<Mitigation> GetMitigationsByRiskId(Guid riskId)
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

        /// <summary>
        /// Updates mitigation
        /// </summary>
        /// <param name="mitigation"></param>
        public void Update(Mitigation mitigation)
        {
            _context.Mitigations.Update(mitigation);
            _context.SaveChanges();
        }
    }
}
