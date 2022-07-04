//using MicroserviceTemplate.Domain;
//using MicroserviceTemplate.Infrastructure.Interfaces;

//namespace MicroserviceTemplate.Infrastructure
//{
//    public class IncidentRepository : IIncidentRepository
//    {

//        public static List<Incident> GetSeedingMessages()
//        {
//            return new List<Incident>()
//            {
//                new Incident(){ Summary = "You're standing on my scarf." },
//                new Incident(){ Summary = "Would you like a jelly baby?" },
//                new Incident(){ Summary = "To the rational mind, nothing is inexplicable; only unexplained." }
//            };
//        }

//        private static readonly string[] Summaries = new[]
//        {
//            "Accident", "Speeding", "Maintenance", "Rain"
//        };       

//        public IEnumerable<Incident> GetIncidents()
//        {
//            return Enumerable.Range(1, 10).Select(index => new Incident
//            {
//                Id = Guid.NewGuid(),
//                Fact = new Profile { Date = GetRandomDateTime(), NumberOfPeopleInvolved = Random.Shared.Next(1, 10) },
//                P = GetRandomType(),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }

//        public Incident GetById(Guid id)
//        {
//            return new Incident
//            {
//                Id = id,
//                Fact = new Profile { Date = GetRandomDateTime(), NumberOfPeopleInvolved = Random.Shared.Next(1, 10) },
//                P = GetRandomType(),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            };
//        }

//        private Permission GetRandomType()
//        {
//            Array values = Enum.GetValues(typeof(Permission));
//            Random random = new Random();
//            Permission randomType = (Permission)values.GetValue(random.Next(values.Length));
//            return randomType;
//        }

//        private DateTime GetRandomDateTime()
//        {
//            Random random = new Random();
//            return new DateTime(random.Next(2000, 2022), random.Next(1, 12), random.Next(1, 31));
//        }
//    }
//}
