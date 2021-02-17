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

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Successful Endpoint");
        }
    }
}
