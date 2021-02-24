using System;
using System.Collections.Generic;
using System.Linq;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Helpers;
using RiskManagementSystem_API.Models.Projects;
using RiskManagementSystem_API.Models.Users;

namespace RiskManagementSystem_API.Services
{
    public interface IProjectService
    {
        void AddTeamMember(TeamMember newMember);
        IEnumerable<Project> GetAll();
        Project GetById(Guid id);
        IEnumerable<Project> GetByUserId(Guid userId);
        Project GetByName(string name);
        IEnumerable<DisplayUserModel> GetNonMembers(Guid pid);
        void AddTeamMembers(string pid, List<string> userIds);
        void Create(Project newProject);
        void Update();
        void Delete(Guid id);
        void DeleteTeamMember(Guid pid, Guid uid);
        IEnumerable<TeamMemberModel> GetTeamByProjectId(Guid projectId);
    }

    public class ProjectService : IProjectService
    {
        private DataContext _context;

        public ProjectService(DataContext context)
        {
            _context = context;
        }

        public void AddTeamMembers(string pidString, List<string> userIds)
        {
            Guid pid = Guid.Parse(pidString);

            if(_context.Projects.Any(x => x.Id.Equals(pid)))
            {
                foreach(string idString in userIds)
                {
                    Guid id = Guid.Parse(idString);
                    TeamMember newMember = new TeamMember()
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = pid,
                        TeamLeader = false,
                        UserId = id
                    };
                    this.AddTeamMember(newMember);
                }
            }
        }

        public void AddTeamMember(TeamMember newMember)
        {
            if (_context.TeamMembers.Any(x => x.UserId.Equals(newMember.UserId) && x.ProjectId.Equals(newMember.ProjectId)))
                throw new AppException("User is already a Team Member");

            _context.TeamMembers.Add(newMember);
            _context.SaveChanges();
        }

        public void Create(Project newProject)
        {
            if (_context.Projects.Any(x => x.Name == newProject.Name))
                throw new AppException("Name \"" + newProject.Name + "\" is already in use");

            _context.Projects.Add(newProject);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var project = _context.Projects.Find(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
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

        public IEnumerable<DisplayUserModel> GetNonMembers(Guid pid)
        {
            IEnumerable<Guid> memberIds = _context.TeamMembers.Where(x => x.ProjectId.Equals(pid)).Select(x => x.UserId);
            var list = from u in _context.Users
                       where !memberIds.Contains(u.Id)
                       select new DisplayUserModel
                       {
                           Id = u.Id,
                           Email = u.Email
                       };
            return list;
        }

        public IEnumerable<TeamMemberModel> GetTeamByProjectId(Guid projectId)
        {
            var list = from m in _context.TeamMembers
                       join u in _context.Users on m.UserId equals u.Id
                       where m.ProjectId.Equals(projectId)
                       select new TeamMemberModel
                       {
                           Id = m.Id,
                           ProjectId = m.ProjectId,
                           TeamLeader = m.TeamLeader,
                           UserId = m.UserId,
                           Name = u.Email
                       };
            return list;
        }

        public void DeleteTeamMember(Guid pid, Guid uid)
        {
            var teamMember = _context.TeamMembers.Where(x => x.UserId.Equals(uid) && x.ProjectId.Equals(pid)).FirstOrDefault();
            if (teamMember != null)
            {
                _context.TeamMembers.Remove(teamMember);
                _context.SaveChanges();
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
    
}
