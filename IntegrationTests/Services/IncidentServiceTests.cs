using System.Threading.Tasks;
using System.Text.Json;
using System.Threading.Tasks;
using MicroserviceTemplate.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using AutoBogus;
using IntegrationTests.Fixtures;
using MicroserviceTemplate.Application;

namespace IntegrationTests.Services
{
    [Collection("IntegrationTests")]
    public class IncidentServiceTests
        : IClassFixture<DatabaseFixture>
    {
        public DatabaseFixture Fixture { get; }
        public IncidentServiceTests(DatabaseFixture fixture)
        => Fixture = fixture;

        [Fact]
        public async Task ServiceTest()
        {
            // Arrange
            var context = Fixture.CreateContext();
            context.Database.BeginTransaction();
            var incidentService = new IncidentService(context);
            // Act

            var incident = new AutoFaker<Incident>();
            await incidentService.SaveNew(incident);
            context.ChangeTracker.Clear();

            // Assert
            var incidents = context.Incidents.ToList(); 
            incidents.Should().HaveCount(4);
        }
    }
}





//using System.Threading.Tasks;
//using System.Text.Json;
//using System.Threading.Tasks;
//using MicroserviceTemplate.Application.Interfaces;
//using Microsoft.Extensions.DependencyInjection;
//using AutoBogus;

//namespace IntegrationTests.Services
//{
//    public class IncidentServiceTests
//        : IClassFixture<CustomWebApplicationFactory<Program>>
//    {
//        private readonly CustomWebApplicationFactory<Program> _factory;
//        private IIncidentService _incidentService;

//        public IncidentServiceTests(CustomWebApplicationFactory<Program> factory)
//        {
//            _factory = factory;


//        }

//        [Fact]
//        public async Task ServiceTest()
//        {
//            // Arrange
//            var client = _factory.CreateClient();

//            var services = new ServiceCollection();
//            //services.AddTransient<IIncidentService, MatchRepositoryStub>();

//            var serviceProvider = services.BuildServiceProvider();

//            _incidentService = serviceProvider.GetService<IIncidentService>();
//            // Act

//            var incident = new AutoFaker<Incident>();
//            _incidentService.SaveNew(incident);
//            var response = await client.GetAsync("/Incident");
//            var incidents = await JsonSerializer.DeserializeAsync<List<Incident>>(response.Content.ReadAsStream());
//            // Assert
//            incidents.Should().HaveCount(4);
//            response.EnsureSuccessStatusCode(); // Status Code 200-299
//        }
//    }
//}