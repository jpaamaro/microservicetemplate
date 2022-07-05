using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        IProfileService profileService; 
        IConfiguration configuration;

        private readonly ILogger<PermissionController> _logger;

        public ProfileController(ILogger<PermissionController> logger, IProfileService profileService, IConfiguration configuration)
        {
            _logger = logger;
            this.profileService = profileService;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Profile>> Get()
        {
            return await profileService.GetProfiles();
        }

        [HttpPost]
        public async Task<IActionResult> AddProfile(Profile profile)
        {
            await profileService.AddProfile(profile);
            return Ok();
        }

        /*
        [HttpGet("/Rainy")]
        public async Task<IEnumerable<Incident>> GetRainy()
        {
            return await permissionService.GetRainyIncidents();
        }

        [HttpGet("/{id}")]
        public Incident GetById(Guid id)
        {
            return permissionService.GetById(id);
        }  
        */
    }
}