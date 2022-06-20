namespace MicroserviceTemplate.Domain.newtypes
{
    public class Incident
    {
        public Guid Id { get; set; }
        public IncidentType Type { get; set; }
        public IncidentFact Fact { get; set; }
        public string? Summary { get; set; }
    }
}
