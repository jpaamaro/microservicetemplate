using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
        IPermissionService permissionService; 
        IConfiguration configuration;

        private readonly ILogger<PermissionController> _logger;

        public PermissionController(ILogger<PermissionController> logger, IPermissionService permissionService, IConfiguration configuration)
        {
            _logger = logger;
            this.permissionService = permissionService;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Permission>> Get()
        {
            return await permissionService.GetPermissions();
        }

        [HttpPost]
        public async Task<IActionResult> AddPermission(Permission permission)
        {
            await permissionService.AddPermission(permission);
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