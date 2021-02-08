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

        [AllowAnonymous]
        [HttpGet("test/{id}")]
        public IActionResult Test(string id)
        {
            Guid riskId = Guid.Parse(id); 
            var riskProperties = _riskService.GetRiskPropertiesForRisk(riskId);
            var model = _mapper.Map<IList<RiskPropertiesModel>>(riskProperties);
            return Ok(model);
        }
    }
}
