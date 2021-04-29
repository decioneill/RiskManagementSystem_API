using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using RiskManagementSystem_API.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using RiskManagementSystem_API.Services;
using RiskManagementSystem_API.Models.Projects;
using System.Collections.Generic;
using System.Linq;
using RiskManagementSystem_API.Entities;
using System;

namespace RiskManagementSystem_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        /// <summary>
        /// Project Service Interface
        /// </summary>
        private IProjectService _projectService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="projectService"></param>
        /// <param name="mapper"></param>
        /// <param name="appSettings"></param>
        public ProjectController(
            IProjectService projectService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _projectService = projectService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Returns project of Id
        /// </summary>
        /// <param name="pid">project id</param>
        /// <returns>project of id</returns>
        [HttpGet("{pid}")]
        public IActionResult GetById(string pid)
        {
            if (!string.IsNullOrEmpty(pid))
            {
                Guid projId = Guid.Parse(pid);
                try
                {
                    Project project = _projectService.GetById(projId);
                    return Ok(project);
                }
                catch (AppException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return null;
        }

        /// <summary>
        /// Creates TeamMember of UserId, ProjectId
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpPost("{pid}/{uid}")]
        public IActionResult AddTeamMember(string pid, string uid)
        {
            Guid projId = Guid.Parse(pid);
            Guid userId = Guid.Parse(uid);
            TeamMember newTeamMember = new TeamMember() { ProjectId = projId, UserId = userId, TeamLeader = false };
            try
            {
                _projectService.AddTeamMember(newTeamMember);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Toggles users Teamleader value
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet("{pid}/{uid}/switchleaderrole")]
        public IActionResult SwitchLeaderRole(string pid, string uid)
        {
            Guid projectId = Guid.Parse(pid);
            Guid userId = Guid.Parse(uid);
            try
            {
                _projectService.SwitchLeaderRole(projectId, userId);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Creates TeamMember entities for list of users for project
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost("{pid}/createTeamMembers")]
        public IActionResult AddTeamMembers(string pid, List<string> users)
        {
            try
            {
                _projectService.AddTeamMembers(pid, users);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Gets a list of all projects and their team.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>List of ProjectTeamModel</returns>
        [HttpGet]
        public IActionResult GetAll(string uid)
        {
            Guid userId = Guid.Parse(uid);
            var projects = _projectService.GetAll(userId).ToList();
            var allList = new List<ProjectTeamModel>();
            foreach(Project project in projects)
            {
                ProjectTeamModel model = new ProjectTeamModel(project);
                model.Team = _projectService.GetTeamByProjectId(model.Id).ToArray();
                allList.Add(model);
            }
            allList = allList.OrderBy(x => x.Name).ToList();
            return Ok(allList.ToArray());
        }

        /// <summary>
        /// Returns list of users not on the team for project of id.
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet("{pid}/getnonmembers")]
        public IActionResult GetNonMembers(string pid)
        {
            Guid projectId = Guid.Parse(pid);
            try 
            { 
                var list = _projectService.GetNonMembers(projectId);
                return Ok(list.ToArray());
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message});
            }
        }

        /// <summary>
        /// Creates a new Project of name
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("createproject")]
        public IActionResult CreateProject([FromBody] ProjectModel model)
        {
            model.Id = Guid.NewGuid();
            // map model to entity
            var newProject = _mapper.Map<Project>(model);
            int x = 6;
            if(model.Name.Length < x)
            {
                // return error message if there was an exception
                return BadRequest(new { message = string.Format("Length should be larger than {0} characters", x) });
            }

            try
            {
                _projectService.Create(newProject);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// deletes project of id
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpDelete("{pid}")]
        public IActionResult Delete(string pid)
        {
            Guid projectId = Guid.Parse(pid);
            _projectService.Delete(projectId);
            return Ok();
        }

        /// <summary>
        /// Deletes team member for project of userid
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpDelete("{pid}/{uid}")]
        public IActionResult DeleteTeamMember(string pid, string uid)
        {
            Guid projectId = Guid.Parse(pid);
            Guid userId = Guid.Parse(uid);
            try
            {
                _projectService.DeleteTeamMember(projectId, userId);
                return Ok();
            } catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all projects that a user has permission to view.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>List of Projects</returns>
        [HttpGet("{uid}/userprojects")]
        public IActionResult GetUsersProjects(string uid)
        {
            Guid userId = Guid.Parse(uid);
            try
            {
                IEnumerable<Project> list = _projectService.GetUserProjects(userId);
                return Ok(list);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
