using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        IIncidentService incidentService; 
        IConfiguration configuration;

        private readonly ILogger<IncidentController> _logger;

        public IncidentController(ILogger<IncidentController> logger, IIncidentService incidentService, IConfiguration configuration)
        {
            _logger = logger;
            this.incidentService = incidentService;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Incident>> Get()
        {
            return await incidentService.GetIncidents();
        }

        [HttpGet("/Rainy")]
        public async Task<IEnumerable<Incident>> GetRainy()
        {
            return await incidentService.GetRainyIncidents();
        }

        [HttpGet("/{id}")]
        public Incident GetById(Guid id)
        {
            return incidentService.GetById(id);
        }

        [HttpGet("/name")]
        public string GetNameFromConfig()
        {
            return configuration["Name"];
        }
    }
}