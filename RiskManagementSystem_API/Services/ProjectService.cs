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
        IEnumerable<Project> GetAll(Guid userId);
        Project GetById(Guid id);
        IEnumerable<DisplayUserModel> GetNonMembers(Guid pid);
        void AddTeamMembers(string pid, List<string> userIds);
        void Create(Project newProject);
        void Delete(Guid id);
        void DeleteTeamMember(Guid pid, Guid uid);
        void SwitchLeaderRole(Guid pid, Guid uid);
        IEnumerable<TeamMemberModel> GetTeamByProjectId(Guid projectId);
        IEnumerable<Project> GetUserProjects(Guid userId);
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
                    if (_context.TeamMembers.Any(x => x.UserId.Equals(id) && x.ProjectId.Equals(pid)))
                    { 
                        break;
                    }
                    else
                    {
                        TeamMember newMember = new TeamMember()
                        {
                            ProjectId = pid,
                            TeamLeader = false,
                            UserId = id
                        };
                        this.AddTeamMember(newMember);
                    }
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

        public IEnumerable<Project> GetAll(Guid userId)
        {
            IEnumerable<Project> projects;
            User user = _context.Users.Find(userId);
            if (user.Admin)
            {
                projects = _context.Projects.ToList();
            }
            else
            {
                var projectIds = _context.TeamMembers.Where(x => x.UserId == userId).Select(x => x.ProjectId);
                var list = from proj in _context.Projects
                           where projectIds.Contains(proj.Id)
                           select new Project
                           {
                               Id = proj.Id,
                               Name = proj.Name
                           };
                projects = list.ToList();
            }
            return projects;
        }

        public Project GetById(Guid pid)
        {
            Project proj = _context.Projects.Where(x => x.Id.Equals(pid)).FirstOrDefault();
            return proj;
        }

        public IEnumerable<DisplayUserModel> GetNonMembers(Guid pid)
        {
            IEnumerable<Guid> memberIds = _context.TeamMembers.Where(x => x.ProjectId.Equals(pid)).Select(x => x.UserId);
            var list = from u in _context.Users
                       where !memberIds.Contains(u.Id)
                       select new DisplayUserModel
                       {
                           Id = u.Id,
                           Username = u.Username
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
                           ProjectId = m.ProjectId,
                           TeamLeader = m.TeamLeader,
                           UserId = m.UserId,
                           Name = u.Username
                       };
            return list.OrderByDescending(x => x.TeamLeader).ThenBy(x => x.Name);
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

        public void SwitchLeaderRole(Guid pid, Guid uid)
        {
            var teamMember = _context.TeamMembers.Where(x => x.UserId.Equals(uid) && x.ProjectId.Equals(pid)).FirstOrDefault();
            if (teamMember != null)
            {
                teamMember.TeamLeader = !teamMember.TeamLeader;
                _context.TeamMembers.Update(teamMember);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Project> GetUserProjects(Guid userId)
        {
            User user = _context.Users.Where(x => x.Id.Equals(userId)).FirstOrDefault();
            List<Guid> projectIds = new List<Guid>();
            if (user.Admin)
            {
                projectIds = _context.Projects.Select(tm => tm.Id).ToList<Guid>();
            }
            else
            {
                projectIds = _context.TeamMembers.Where(user => user.UserId.Equals(userId)).Select(tm => tm.ProjectId).ToList<Guid>();
            }
            var list = from p in _context.Projects
                       where projectIds.Contains(p.Id)
                       select new Project
                       {
                           Id = p.Id,
                           Name = p.Name
                       };
            return list.OrderBy(x => x.Name);
        }
    }
    
}
