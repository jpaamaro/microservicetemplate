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