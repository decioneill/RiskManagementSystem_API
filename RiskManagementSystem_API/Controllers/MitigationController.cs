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
                IEnumerable<Mitigation> mitigations = _mitigationService.GetMitigationsByRiskId(riskId);
                return Ok(mitigations);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{mid}")]
        public IActionResult GetMitigationById(string mid)
        {
            Guid mitigationId = Guid.Parse(mid);
            try
            {
                Mitigation mitigations = _mitigationService.GetMitigationById(mitigationId);
                return Ok(mitigations);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("newmitigation/{rid}")]
        public IActionResult CreateMitigation(string rid, [FromBody] Mitigation mitigation)
        {
            mitigation.Id = Guid.NewGuid();
            Guid riskId = Guid.Parse(rid);
            MitigationRisk mr = new MitigationRisk()
            {
                MitigationId = mitigation.Id,
                RiskId = riskId
            };
            try
            {
                _mitigationService.Create(mitigation, mr);
                return Ok(mitigation);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{mid}")]
        public IActionResult UpdateMitigation(string mid, [FromBody] Mitigation mitigation)
        {
            mitigation.Id = Guid.Parse(mid);
            try
            {
                _mitigationService.Update(mitigation);
                return Ok(mitigation);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{mid}/{rid}")]
        public IActionResult DeleteFromRisk(string mid, string rid)
        {
            Guid mitigationId = Guid.Parse(mid);
            Guid riskId = Guid.Parse(rid);
            try
            {
                _mitigationService.DeleteFromRisk(mitigationId, riskId);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
