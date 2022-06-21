using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using FluentAssertions;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using MicroserviceTemplate.Domain;

namespace UnitTest
{
    public class ArchitectureTest
    {
        private static readonly string DomainLayerRule = "Domain Layer cannot depend on outer layers.";
        private static readonly string DomainLayerNamespace = ".Domain*";
        private static readonly string DomainLayerName = "Domain Layer";

        private static readonly string ApplicationLayerNamespace = ".Application*";
        private static readonly string ApplicationLayerName = "Application Layer";

        private static readonly string ApiLayerNamespace = ".Application*";
        private static readonly string ApiLayerName = "API Layer";

        private static readonly string InfrastructureLayerNamespace = ".Infrastructure*";
        private static readonly string InfrastructureLayerName = "Infrastructure Layer";

        private static readonly string ServiceClassesName = "Service Classes";
        private static readonly string ServiceName = "Service";
        private static readonly string RepositoryInterfacesName = "Repository Interfaces";
        private static readonly string RepositoryName = "Repository";

        private static readonly System.Reflection.Assembly Assembly = typeof(Incident).Assembly;        // replace Incident with generic microservice class

        // load architecture once at the start to maximize performance of your tests
        private static readonly Architecture Architecture =
            new ArchLoader().LoadAssemblies(Assembly)
            .Build();

        private static readonly string AssemblyName = Assembly.GetName().ToString().Split(",")[0];       

        // Application Layers

        private readonly IObjectProvider<IType> DomainLayer =
           Types().That().ResideInNamespace(AssemblyName + DomainLayerNamespace, true).As(DomainLayerName);

        private readonly IObjectProvider<IType> ApplicationLayer =
           Types().That().ResideInNamespace(AssemblyName + ApplicationLayerNamespace, true).As(ApplicationLayerName);        

        private readonly IObjectProvider<IType> InfrastructureLayer =
            Types().That().ResideInNamespace(AssemblyName + InfrastructureLayerNamespace, true).As(InfrastructureLayerName);

        private readonly IObjectProvider<IType> ApiLayer =
          Types().That().ResideInNamespace(AssemblyName + ApiLayerNamespace, true).As(ApiLayerName);

        // Classes and Interfaces

        private readonly IObjectProvider<Class> ServiceClasses =
          Classes().That().HaveNameContaining(ServiceName).As(ServiceClassesName);

        private readonly IObjectProvider<Interface> RepositoryInterfaces =
            Interfaces().That().HaveFullNameContaining(RepositoryName).As(RepositoryInterfacesName);

        // Tests

        [Fact]
        public void TypesShouldBeInCorrectLayer()
        {
            // Arrange 
            IArchRule serviceClassesShouldBeInApplicationLayer =
                Classes().That().Are(ServiceClasses).Should().Be(ApplicationLayer);
            IArchRule repositoryInterfacesShouldBeInInfrastructureLayer =
                Interfaces().That().Are(RepositoryInterfaces).Should().Be(InfrastructureLayer);                 

            // Act           
            IArchRule combinedArchRule =
                serviceClassesShouldBeInApplicationLayer.And(repositoryInterfacesShouldBeInInfrastructureLayer);
            var result = combinedArchRule.Evaluate(Architecture);

            // assert
            result.Where(x => x.Passed == true).Should().NotBeEmpty();
        }

        [Fact]
        public void DomainLayerShouldNotAccessOtherLayers()
        {
            // Arrange
            IArchRule domainLayerShouldNotAccessApplicationLayer = Types().That().Are(DomainLayer).Should()
                .NotDependOnAny(ApplicationLayer).Because(DomainLayerRule);

            IArchRule domainLayerShouldNotAccessInfracstrutureLayer = Types().That().Are(DomainLayer).Should()
              .NotDependOnAny(InfrastructureLayer).Because(DomainLayerRule);

            IArchRule domainLayerShouldNotAccessApiLayer = Types().That().Are(DomainLayer).Should()
                .NotDependOnAny(ApplicationLayer).Because(DomainLayerRule);             

            // Act
            var result = domainLayerShouldNotAccessApplicationLayer
                .And(domainLayerShouldNotAccessInfracstrutureLayer)
                .And(domainLayerShouldNotAccessApiLayer)
                .Evaluate(Architecture);

            // Asserts
            result.Where(x => x.Passed == true).Should().NotBeEmpty();
        }

        [Fact]
        public void RepositoryClassesShouldHaveCorrectName()
        {
            // Arrange
            var repositoryInterfacesShouldHaveRepositoryName = Classes().That().AreAssignableTo(RepositoryInterfaces).Should().HaveNameContaining("Repository");

            // Act
            var result = repositoryInterfacesShouldHaveRepositoryName.Evaluate(Architecture);

            // Assert
            result.Where(x => x.Passed == true).Should().NotBeEmpty();
        }

        [Fact]
        public void InfrastructureClassesShouldNotCallApplicationMethods()
        {
            // Arrange
            var repositoryShouldNotCallApplication = Classes().That().Are(RepositoryInterfaces).Should().NotCallAny(
                    MethodMembers().That().AreDeclaredIn(ApplicationLayer));

            // Act
            var result = repositoryShouldNotCallApplication
                .Evaluate(Architecture);            

            // Assert           
            result.Where(x => x.Passed == true).Should().NotBeEmpty();
        }
    }
}