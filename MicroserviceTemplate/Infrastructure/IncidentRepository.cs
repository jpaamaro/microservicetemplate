using MicroserviceTemplate.Domain;
using MicroserviceTemplate.Infrastructure.Interfaces;

namespace MicroserviceTemplate.Infrastructure
{
    public class IncidentRepository : IIncidentRepository
    {
        private static readonly string[] Summaries = new[]
        {
            "Accident", "Speeding", "Maintenance", "Rain"
        };       

        public IEnumerable<Incident> GetIncidents()
        {
            return Enumerable.Range(1, 10).Select(index => new Incident
            {
                Id = Guid.NewGuid(),
                Fact = new IncidentFact { Date = GetRandomDateTime(), NumberOfPeopleInvolved = Random.Shared.Next(1, 10) },
                Type = GetRandomType(),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public Incident GetById(Guid id)
        {
            return new Incident
            {
                Id = id,
                Fact = new IncidentFact { Date = GetRandomDateTime(), NumberOfPeopleInvolved = Random.Shared.Next(1, 10) },
                Type = GetRandomType(),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        private IncidentType GetRandomType()
        {
            Array values = Enum.GetValues(typeof(IncidentType));
            Random random = new Random();
            IncidentType randomType = (IncidentType)values.GetValue(random.Next(values.Length));
            return randomType;
        }

        private DateTime GetRandomDateTime()
        {
            Random random = new Random();
            return new DateTime(random.Next(2000, 2022), random.Next(1, 12), random.Next(1, 31));
        }
    }
}
