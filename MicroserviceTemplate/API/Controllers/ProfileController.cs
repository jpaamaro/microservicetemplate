using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTemplate.API.Controllers
{
    [ApiController]
    [Route("/accounts/profiles")]
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

        #region Endpoints

        [HttpGet]
        public async Task<IEnumerable<Profile>> Get(int skip = 0, int top = -1, string searchText = "")
        {

            var profiles = await profileService.GetProfiles();

            if (skip != 0)
            {
                profiles = profiles.Skip(skip);
            }
            if (top > 0)
            {
                profiles = profiles.Take(top);
            }
            if (searchText.Length > 0)
            {
                profiles = profiles.Where(x => x.Name.Contains(searchText));
            }

            return profiles;
        }

        [HttpPost]
        public async Task<IActionResult> AddProfile(Profile profile)
        {
            await profileService.AddProfile(profile);
            return Ok();
        }

        [HttpGet("{profileId}")]
        public async Task<ActionResult<Profile>> GetById(Guid profileId)
        {
            var result = await profileService.GetById(profileId);
            if (result == null)
                return NotFound();
            return result;
        }

        /*
        [HttpPost("{profileId}/AddPermission")]
        public async Task<IActionResult> AddPermissionToProfile(Guid profileId, Guid permissionId)
        {
            await profileService.AddPermissionToProfile(profileId, permissionId);
            return Ok();
        }
        */

        [HttpPut("{profileId}")]
        public async Task<IActionResult> UpdateProfile(Guid profileId, Profile profile)
        {
            return await profileService.UpdateProfile(profileId, profile);
        }

        [HttpDelete("{profileId}")]
        public async Task<IActionResult> DeleteProfile(Guid profileId)
        {
            return await profileService.DeleteProfileAsync(profileId);
        }       

        #endregion
    }
}