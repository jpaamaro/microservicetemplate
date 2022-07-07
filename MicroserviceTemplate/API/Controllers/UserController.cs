using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTemplate.API.Controllers
{
    [ApiController]
    [Route("/accounts/users")]
    public class UserController : ControllerBase
    {
        IUserService userService; 
        IConfiguration configuration;

        private readonly ILogger<PermissionController> _logger;

        public UserController(ILogger<PermissionController> logger, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
            this.userService = userService;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await userService.GetUsers();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetById(Guid userId)
        {
            var result = await userService.GetById(userId);
            if (result == null)
                return NotFound();
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            await userService.AddUser(user);
            return Ok();
        }

        [HttpPost("{userId}/profiles")]
        public async Task<IActionResult> AddProfileToUser(Guid profileId, Guid userId)
        {
            return await userService.AddProfileToUser(profileId, userId);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            return await userService.DeleteUserAsync(userId);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, User user)
        {
            return await userService.UpdateUser(userId, user);
        }
    }
}