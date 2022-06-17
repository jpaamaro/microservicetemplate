using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using MicroserviceTemplate.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        IIncidentService incidentService; 

        private readonly ILogger<IncidentController> _logger;

        public IncidentController(ILogger<IncidentController> logger, IIncidentService incidentService)
        {
            _logger = logger;
            this.incidentService = incidentService;
        }

        [HttpGet]        
        public IEnumerable<Incident> Get()
        {
           return incidentService.GetIncidents();
        }

        [HttpGet("/Rainy")]
        public IEnumerable<Incident> GetRainy()
        {
            return incidentService.GetRainyIncidents();
        }

        [HttpGet("/{id}")]
        public Incident GetById(Guid id)
        {
            return incidentService.GetById(id);
        }      
    }
}