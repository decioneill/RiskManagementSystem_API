using System;
using System.Collections.Generic;
using System.Linq;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Helpers;

namespace RiskManagementSystem_API.Services
{
    public interface IProjectService
    {        
        IEnumerable<Project> GetAll();
        Project GetById(Guid id);
        IEnumerable<Project> GetByUserId(Guid userId);
        Project GetByName(string name);
        Project Create();
        void Update();
        void Delete(Guid id);
    }

    public class ProjectService : IProjectService
    {
        private DataContext _context;

        public ProjectService(DataContext context)
        {
            _context = context;
        }

        public Project Create()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAll()
        {
            var projects = _context.Projects;
            return projects;
        }

        public Project GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Project GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
    
}
