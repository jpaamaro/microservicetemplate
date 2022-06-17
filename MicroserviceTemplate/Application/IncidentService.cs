using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using MicroserviceTemplate.Infrastructure.Interfaces;

namespace MicroserviceTemplate.Application
{
    public class IncidentService : IIncidentService
    {
        IIncidentRepository incidentRepository;
        const string RAIN = "Rain";

        public IncidentService(IIncidentRepository incidentRepository)
        {
            this.incidentRepository = incidentRepository;
        }

        public IEnumerable<Incident> GetIncidents()
        {
           return incidentRepository.GetIncidents();
        }

        public IEnumerable<Incident> GetRainyIncidents()
        {
            var incidents = incidentRepository.GetIncidents();
            return incidents.Where(x => x.Summary == RAIN);
        }

        public Incident GetById(Guid id)
        {
            return incidentRepository.GetById(id);
        }
    }
}
