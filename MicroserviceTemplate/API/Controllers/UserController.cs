using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            await userService.AddUser(user);
            return Ok();
        }

        [HttpPost("/AddProfile")]
        public async Task<IActionResult> AddProfileToUser(Guid profileId, Guid userId)
        {
            await userService.AddProfileToUser(profileId, userId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await userService.DeleteAllUsersAsync();
            return Ok();
        }
    }
}