using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MicroserviceTemplate.Domain;


namespace MicroserviceTemplate.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Incident> Incidents { get; set; } = null!;
        public virtual DbSet<IncidentFact> IncidentFact { get; set; } = null!;


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Atlasdb;User Id=myusername;Password=mypassword");
        //}

        public void Initialize()
        {
            Incidents.AddRange(GetSeedingMessages());
            SaveChanges();
        }

        public static List<Incident> GetSeedingMessages()
        {
            return new List<Incident>()
            {
                new Incident
                {
                    Fact = new IncidentFact{
                        Date = DateTime.Now,
                        NumberOfPeopleInvolved = 1,
                    },
                     Id = Guid.NewGuid(),
                     Type = IncidentType.TypeTwo,
                     Summary = "Accident"
                },
                 new Incident
                {
                     Fact = new IncidentFact{
                        Date = DateTime.Now,
                        NumberOfPeopleInvolved = 5,
                    },
                     Id = Guid.NewGuid(),
                     Type = IncidentType.TypeOne,
                     Summary = "Rain"
                },
                    new Incident
                {
                     Fact = new IncidentFact{
                        Date = DateTime.Now,
                        NumberOfPeopleInvolved = 3,
                    },
                     Id = Guid.NewGuid(),
                     Type = IncidentType.TypeThree,
                     Summary = "Maintenance"
                },
            };
        }
    }
}
