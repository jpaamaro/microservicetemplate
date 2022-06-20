
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using FluentAssertions;

//add a using directive to ArchUnitNET.Fluent.ArchRuleDefinition to easily define ArchRules
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using MicroserviceTemplate.Application;
using MicroserviceTemplate.Domain;

namespace UnitTest
{
    public class ArchitectureTest
    {
        // TIP: load your architecture once at the start to maximize performance of your tests
        private static readonly Architecture Architecture =
            new ArchLoader().LoadAssemblies(typeof(Incident).Assembly)
            .Build();

        // might need a try catch
        private static readonly string AssemblyName = typeof(Incident).Assembly.GetName().ToString().Split(",")[0];

        // replace <ExampleClass> and <ForbiddenClass> with classes from the assemblies you want to test

        //declare variables you'll use throughout your tests up here
        //use As() to give them a custom description

        /*
        private readonly IObjectProvider<IType> ApplicationLayer =
            Types().That().ResideInAssembly("ExampleAssembly").As("Application Layer"); // what assembly?

        private readonly IObjectProvider<Class> ExampleClasses =
            Classes().That().ImplementInterface("IExampleInterface").As("Example Classes");
        */


        private readonly IObjectProvider<IType> DomainLayer =
           Types().That().ResideInNamespace(AssemblyName + ".Domain*", true).As("Domain Layer");

        private readonly IObjectProvider<IType> ApplicationLayer =
           Types().That().ResideInNamespace("MicroserviceTemplate.Application*", true).As("Application Layer");

        private readonly IObjectProvider<Class> ServiceClasses =
            Classes().That().HaveFullNameContaining("application").As("Service Classes");       

        private readonly IObjectProvider<IType> InfrastructureLayer =
            Types().That()
            .ResideInNamespace("MicroserviceTemplate.Infrastructure*", true)
            //.ResideInNamespace("MicroserviceTemplate.Infrastructure.Interfaces")
            .As("Infrastructure Layer");

        private readonly IObjectProvider<Interface> RepositoryInterfaces =
            Interfaces().That().HaveFullNameContaining("repository").As("Repository Interfaces");


        //write some tests
        [Fact]
        public void TypesShouldBeInCorrectLayer()
        {
            //you can use the fluent API to write your own rules
            // Arrange 
            IArchRule serviceClassesShouldBeInApplicationLayer =
                Classes().That().Are(ServiceClasses).Should().Be(ApplicationLayer);
            IArchRule repositoryInterfacesShouldBeInInfrastructureLayer =
                Interfaces().That().Are(RepositoryInterfaces).Should().Be(InfrastructureLayer);                 

            // Act

            //check if your architecture fulfils your rules
            var res1 = serviceClassesShouldBeInApplicationLayer.HasNoViolations(Architecture);
            var ev1 = serviceClassesShouldBeInApplicationLayer.Evaluate(Architecture);

            var res2 = repositoryInterfacesShouldBeInInfrastructureLayer.HasNoViolations(Architecture);
            var ev2 = repositoryInterfacesShouldBeInInfrastructureLayer.Evaluate(Architecture);

            //you can also combine your rules
            IArchRule combinedArchRule =
                serviceClassesShouldBeInApplicationLayer.And(repositoryInterfacesShouldBeInInfrastructureLayer);
            var result = combinedArchRule.HasNoViolations(Architecture);
            var ev = combinedArchRule.Evaluate(Architecture);

            // assert
            res1.Should().Be(true);
            ev1.Where(x => x.Passed == true).Should().NotBeEmpty();
            res2.Should().Be(true);
            ev2.Where(x => x.Passed == true).Should().NotBeEmpty();
            result.Should().Be(true);
            ev.Where(x => x.Passed == true).Should().NotBeEmpty();
        }

        [Fact]
        public void DomainLayerShouldNotAccessApplicationLayer()
        {
            //you can give your rules a custom reason, which is displayed when it fails
            //(together with the types that failed the rule)
            // Arrange
            IArchRule domainLayerShouldNotAccessApplicationLayer = Types().That().Are(DomainLayer).Should()
                .NotDependOnAny(ApplicationLayer).Because("it's forbidden");

            // Act
            var ev = domainLayerShouldNotAccessApplicationLayer.Evaluate(Architecture);
            var res = domainLayerShouldNotAccessApplicationLayer.HasNoViolations(Architecture);

            // Assert
            res.Should().Be(true);
            ev.Where(x => x.Passed == true).Should().NotBeEmpty();
        }

        [Fact]
        public void RepositoryClassesShouldHaveCorrectName()
        {
            // Arrange
            var repositoryInterfacesShouldHaveRepositoryName = Classes().That().AreAssignableTo(RepositoryInterfaces).Should().HaveNameContaining("Repository");

            // Act
            var ev = repositoryInterfacesShouldHaveRepositoryName.Evaluate(Architecture);
            var res = repositoryInterfacesShouldHaveRepositoryName.HasNoViolations(Architecture);

            // Assert
            res.Should().Be(true, "because yes");
            ev.Where(x => x.Passed == true).Should().NotBeEmpty();
        }

        [Fact]
        public void InfrastructureClassesShouldNotCallApplicationMethods()
        {
            // Arrange
            var repositoryShouldNotCallApplicationOrServices = Classes().That().Are(RepositoryInterfaces).Should().NotCallAny(
                    MethodMembers().That().AreDeclaredIn(ApplicationLayer).Or().HaveNameContaining("service"));

            // Act
            var ev = repositoryShouldNotCallApplicationOrServices
                .Evaluate(Architecture);
            var res = repositoryShouldNotCallApplicationOrServices
               .HasNoViolations(Architecture);



            // Assert
            res.Should().Be(true);
            ev.Where(x => x.Passed == true).Should().NotBeEmpty();
        }
    }
}