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
        Project GetById(int id);
        IEnumerable<Project> GetByUserId();
        Project GetByName(string email);
        Project Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }

    public class ProjectService : IProjectService
    {
        private DataContext _context;

        public ProjectService(DataContext context)
        {
            _context = context;
        }

        public Project Create(User user, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAll()
        {
            var projects = _context.Projects;
            return projects;
        }

        public Project GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Project GetByName(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetByUserId()
        {
            throw new NotImplementedException();
        }

        public void Update(User user, string password = null)
        {
            throw new NotImplementedException();
        }
    }
    
}
