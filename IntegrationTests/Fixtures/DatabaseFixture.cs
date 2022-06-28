using AutoBogus;
using MicroserviceTemplate.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        private const string ConnectionString = "Server=localhost;Port=5433;Database=Atlasdb-test;User Id=myusername;Password=mypassword";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;
        private IEnumerable<Incident> _incidents;

        public DatabaseFixture()
        {
            lock (_lock)
            {
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        var autofaker = new AutoFaker<Incident>();
                        _incidents = autofaker.Generate(3);
                        context.Incidents.AddRange(_incidents);
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public ApplicationDbContext CreateContext()
       => new ApplicationDbContext(
           new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseNpgsql(ConnectionString)
               .Options);

        public void Dispose()
        {
            // ... clean up test data from the database ...        }
        }
    }
}