using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using MicroserviceTemplate.Infrastructure.Interfaces;
using Moq;
using FluentAssertions;
using MicroserviceTemplate.Application;

namespace UnitTest
{
    public class IncidentServiceTest
    {
        IIncidentService incidentService;
        Mock<IIncidentRepository> incidentRepositoryMock;

        public IncidentServiceTest()
        {            
            incidentRepositoryMock = new Mock<IIncidentRepository>();
            incidentService = new IncidentService(incidentRepositoryMock.Object);
        }

        [Fact]
        public void GetIncidents_ReturnsListOfIncidents_ReturnOK()
        {
            // Arrange
            var incidents = GetIncidentList();
            incidentRepositoryMock.Setup(x => x.GetIncidents()).Returns(incidents);

            // Act
            var result = incidentService.GetIncidents();

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(incidents)
                .And.HaveCount(3);
        }

        [Fact]
        public void GetIncidents_ReturnsListOfIncidents_ReturnNotOK()
        {
            // Arrange
            var incidents = GetIncidentList();
            incidentRepositoryMock.Setup(x => x.GetIncidents()).Returns(new List<Incident>{ });

            // Act
            var result = incidentService.GetIncidents();

            // Assert
            result.Should().BeEmpty()
                .And.NotEqual(incidents)
                .And.HaveCount(0);
        }

        #region Private Methods

        private IEnumerable<Incident> GetIncidentList()
        {
            return new List<Incident>
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

        #endregion 
    }
}