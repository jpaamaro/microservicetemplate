//using System.Text.Json;
//using System.Threading.Tasks;

//namespace IntegrationTests.Controllers
//{
//    [Collection("IntegrationTests")]
//    public class IncidentControllerTests
//        : IClassFixture<CustomWebApplicationFactory<Program>>
//    {
//        private readonly CustomWebApplicationFactory<Program> _factory;

//        public IncidentControllerTests(CustomWebApplicationFactory<Program> factory)
//        {
//            _factory = factory;
//        }

//        [Fact]
//        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
//        {
//            // Arrange
//            var client = _factory.CreateClient();

//            // Act
//            var response = await client.GetAsync("/Incident");
//            var incidents = await JsonSerializer.DeserializeAsync<List<Incident>>(response.Content.ReadAsStream());
//            // Assert
//            incidents.Should().HaveCount(3);
//            response.EnsureSuccessStatusCode(); // Status Code 200-299
//        }
//    }
//}

