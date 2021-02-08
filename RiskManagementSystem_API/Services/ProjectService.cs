﻿using System;
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
        IEnumerable<Project> GetByUserId(int userId);
        Project GetByName(string name);
        Project Create();
        void Update();
        void Delete(int id);
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

        public Project GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
    
}
