namespace MicroserviceTemplate.Domain
{
    public class IncidentFact
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfPeopleInvolved { get; set; }

    }
}