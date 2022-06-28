using MicroserviceTemplate.Domain;

namespace MicroserviceTemplate.Application.Interfaces
{
    public interface IIncidentService
    {
        public Task<IEnumerable<Incident>> GetIncidents();
        public Task<IEnumerable<Incident>> GetRainyIncidents();
        public Incident GetById(Guid id);
        public Task SaveNew(Incident item);
    }
}