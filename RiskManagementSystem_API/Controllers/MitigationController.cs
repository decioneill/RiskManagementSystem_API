using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using RiskManagementSystem_API.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using RiskManagementSystem_API.Services;
using RiskManagementSystem_API.Entities;
using System;
using System.Collections.Generic;

namespace RiskManagementSystem_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MitigationController : Controller
    {

        private IMitigationService _mitigationService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public MitigationController(
            IMitigationService mitigationService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _mitigationService = mitigationService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{rid}/mitigations")]
        public IActionResult GetMitigationsByRiskId(string rid)
        {
            Guid riskId = Guid.Parse(rid);
            try
            {
                IEnumerable<Mitigation> mitigations = _mitigationService.GetByRiskId(riskId);
                return Ok(mitigations);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
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
