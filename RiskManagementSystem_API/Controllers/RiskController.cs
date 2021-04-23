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

        [HttpGet("{rid}")]
        public IActionResult GetRiskById(string rid)
        {
            if (!string.IsNullOrEmpty(rid) && rid != "0")
            {
                Guid riskId = Guid.Parse(rid);
                List<Risk> risk = new List<Risk>();
                risk.Add(_riskService.GetRiskById(riskId));
                return Ok(risk);
            }
            return null;
        }

        [HttpGet("{rid}/riskproperties")]
        public IActionResult GetRiskProperties(string rid)
        {
            if (!string.IsNullOrEmpty(rid) && rid != "0")
            {
                Guid riskId = Guid.Parse(rid);
                IEnumerable<RiskProperty> riskProperties = _riskService.GetRiskPropertiesForRisk(riskId);
                return Ok(riskProperties);
            }
            return null;
        }

        [HttpPost("newrisk/{pid}")]
        public IActionResult CreateRisk(string pid, [FromBody] Risk risk)
        {
            risk.Id = Guid.NewGuid();
            risk.ProjectId = Guid.Parse(pid);
            try
            {
                _riskService.Create(risk);
                return Ok(risk);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{rid}")]
        public IActionResult UpdateRisk(string rid, [FromBody] FullRisk fullRisk)
        {
            fullRisk.Id = Guid.Parse(rid);
            Risk risk = fullRisk.GetRisk();
            List<RiskProperty> riskProperties = ProduceRiskModelProperties(fullRisk);
            try
            {
                _riskService.UpdateProperties(riskProperties);
                _riskService.Update(risk);
                return Ok(risk);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        private List<RiskProperty> ProduceRiskModelProperties(FullRisk fullRisk)
        {
            List<RiskProperty> riskProperties = new List<RiskProperty>();
            foreach (RiskScoreTypes scoreType in Enum.GetValues(typeof(RiskScoreTypes)))
            {
                RiskScoreModel risk = fullRisk.GetRiskScore(scoreType);
                CreateRiskPropertiesFromRiskModel(fullRisk.Id, risk, ref riskProperties);
            }

            return riskProperties;
        }

        private void CreateRiskPropertiesFromRiskModel(Guid riskId, RiskScoreModel riskScore, ref List<RiskProperty> riskProperties)
        {
            int impactEnum = 0;
            int likelihoodEnum = 0;
            switch (riskScore.ScoreType)
            {
                case RiskScoreTypes.InherentRiskScore:
                    impactEnum = (int)RiskPropertyTypes.InherentImpact;
                    likelihoodEnum = (int)RiskPropertyTypes.InherentLikelihood;
                    break;
                case RiskScoreTypes.ResidualRiskScore:
                    impactEnum = (int)RiskPropertyTypes.ResidualImpact;
                    likelihoodEnum = (int)RiskPropertyTypes.ResidualLikelihood;
                    break;
                case RiskScoreTypes.FutureRiskScore:
                    impactEnum = (int)RiskPropertyTypes.FutureImpact;
                    likelihoodEnum = (int)RiskPropertyTypes.FurtureLikelihood;
                    break;
            }

            riskProperties.Add(new RiskProperty()
            {
                RiskId = riskId,
                PropertyId = impactEnum,
                PropertyValue = (riskScore.Impact != 0)? riskScore.Impact.ToString() : "1"
            });
            riskProperties.Add(new RiskProperty()
            {
                RiskId = riskId,
                PropertyId = likelihoodEnum,
                PropertyValue = (riskScore.Likelihood != 0) ? riskScore.Likelihood.ToString() : "1"
            });
        }

        [HttpDelete("{rid}")]
        public IActionResult DeleteRisk(string rid)
        {
            Guid id = Guid.Parse(rid);
            try
            {
                _riskService.Delete(id);
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
