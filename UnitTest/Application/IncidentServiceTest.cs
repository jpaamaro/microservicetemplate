//using MicroserviceTemplate.Application.Interfaces;
//using MicroserviceTemplate.Domain;
//using MicroserviceTemplate.Infrastructure.Interfaces;
//using MicroserviceTemplate.Application;
//using MicroserviceTemplate.Infrastructure;

//namespace UnitTest
//{
//    public class IncidentServiceTest
//    {
//        private IPermissionService incidentService;
//        private Mock<ApplicationDbContext> appContextMock;

//        public IncidentServiceTest()
//        {            
//            appContextMock = new Mock<ApplicationDbContext>();
//            incidentService = new IncidentService(appContextMock.Object);
//        }

//        //[Fact]
//        //public void GetIncidents_ReturnsListOfIncidents_ReturnOK()
//        //{
//        //    // Arrange
//        //    var incidents = GetIncidentList();
//        //    appContextMock.Setup(x => x.GetIncidents()).Returns(incidents);

//        //    // Act
//        //    var result = incidentService.GetIncidents();

//        //    // Assert
//        //    result.Should().NotBeNull()
//        //        .And.BeEquivalentTo(incidents)
//        //        .And.HaveCount(3);
//        //}

//        //[Fact]
//        //public void GetIncidents_ReturnsListOfIncidents_ReturnNotOK()
//        //{
//        //    // Arrange
//        //    var incidents = GetIncidentList();
//        //    appContextMock.Setup(x => x.GetIncidents()).Returns(new List<Incident> { });

//        //    // Act
//        //    var result = incidentService.GetIncidents();

//        //    // Assert
//        //    result.Should().BeEmpty()
//        //        .And.NotEqual(incidents)
//        //        .And.HaveCount(0);
//        //}

//        #region Private Methods

//        private IEnumerable<Incident> GetIncidentList()
//        {
//            return new List<Incident>
//            {
//                new Incident
//                {
//                    Fact = new Profile{
//                        Date = DateTime.Now,
//                        NumberOfPeopleInvolved = 1,
//                    },
//                     Id = Guid.NewGuid(),
//                     P = Permission.TypeTwo,
//                     Summary = "Accident"
//                },
//                 new Incident
//                {
//                     Fact = new Profile{
//                        Date = DateTime.Now,
//                        NumberOfPeopleInvolved = 5,
//                    },
//                     Id = Guid.NewGuid(),
//                     P = Permission.TypeOne,
//                     Summary = "Rain"
//                },
//                    new Incident
//                {
//                     Fact = new Profile{
//                        Date = DateTime.Now,
//                        NumberOfPeopleInvolved = 3,
//                    },
//                     Id = Guid.NewGuid(),
//                     P = Permission.TypeThree,
//                     Summary = "Maintenance"
//                },
//            };
//        }

//        #endregion 
//    }
//}