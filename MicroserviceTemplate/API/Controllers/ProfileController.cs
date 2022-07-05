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

        [HttpPost("/AddPermission")]
        public async Task<IActionResult> AddPermissionToProfile(Guid profileId, Guid permissionId)
        {
            await profileService.AddPermissionToProfile(profileId, permissionId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await profileService.DeleteAllProfilesAsync();
            return Ok();
        }      
    }
}