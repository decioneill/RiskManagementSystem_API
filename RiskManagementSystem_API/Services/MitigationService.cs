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
        Mitigation GetById(int id);
        IEnumerable<Mitigation> GetByRiskId(int riskId);
        Mitigation Create();
        void Update();
        void Delete(int id);
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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mitigation> GetAll()
        {
            throw new NotImplementedException();
        }

        public Mitigation GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mitigation> GetByRiskId(int riskId)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
