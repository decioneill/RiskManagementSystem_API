using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using RiskManagementSystem_API.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using RiskManagementSystem_API.Services;


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

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Successful Endpoint");
        }
    }
}
