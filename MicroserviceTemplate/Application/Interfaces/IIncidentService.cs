using MicroserviceTemplate.Domain;

namespace MicroserviceTemplate.Application.Interfaces
{
    public interface IIncidentService
    {
        public IEnumerable<Incident> GetIncidents();
        public IEnumerable<Incident> GetRainyIncidents();
        public Incident GetById(Guid id);
    }
}