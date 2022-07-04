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
        private IEnumerable<Permission> _permissions;

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

                        var autofaker = new AutoFaker<Permission>();
                        _permissions = autofaker.Generate(3);
                        context.Permissions.AddRange(_permissions);
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