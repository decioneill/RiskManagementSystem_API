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
        private IProjectService _projectService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ProjectController(
            IProjectService projectService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _projectService = projectService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost("{pid}/{uid}")]
        public IActionResult AddTeamMember(string pid, string uid)
        {
            Guid projId = Guid.Parse(pid);
            Guid userId = Guid.Parse(uid);
            TeamMember newTeamMember = new TeamMember() { Id = Guid.NewGuid(), ProjectId = projId, UserId = userId, TeamLeader = false };

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var projects = _projectService.GetAll().ToList();
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

        [HttpDelete("{pid}")]
        public IActionResult Delete(string pid)
        {
            Guid projectId = Guid.Parse(pid);
            _projectService.Delete(projectId);
            return Ok();
        }

        [HttpDelete("{pid}/{uid}")]
        public IActionResult Delete(string pid, string uid)
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

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Successful Endpoint");
        }
    }
}
