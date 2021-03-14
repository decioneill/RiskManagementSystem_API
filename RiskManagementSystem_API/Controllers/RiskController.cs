using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using RiskManagementSystem_API.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using RiskManagementSystem_API.Services;
using System.Collections;
using RiskManagementSystem_API.Models.Risks;
using System.Collections.Generic;
using System;
using RiskManagementSystem_API.Entities;

namespace RiskManagementSystem_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RiskController : Controller
    {
        private IRiskService _riskService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public RiskController(
            IRiskService riskService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _riskService = riskService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{pid}/{uid}/short")]
        public IActionResult GetSimpleRisksByUserId(string pid, string uid)
        {
            if (!string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(uid))
            {
                Guid projectId = Guid.Parse(pid);
                Guid userId = Guid.Parse(uid);
                IEnumerable<SimpleRisk> list = _riskService.GetSimpleRisks(projectId, userId);
                return Ok(list);
            }
            return null;
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            Risk newRisk = new Risk()
            {
                Id = Guid.NewGuid(),
                Description = "Test Risk",
                ShortDescription = "Test Short Risk"
            };
            _riskService.Create(newRisk);
            return Ok();
        }
    }
}
