﻿using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using MicroserviceTemplate.Domain;
using ArchUnitNET.xUnit;

namespace UnitTest
{
    public class ArchitectureTest
    {
        private static readonly string DomainLayerRule = "Domain Layer cannot depend on outer layers.";
        private static readonly string DomainLayerNamespace = ".Domain*";
        private static readonly string DomainLayerName = "Domain Layer";

        private static readonly string ApplicationLayerRule = "Application Layer cannot depend on API layer.";
        private static readonly string ApplicationLayerNamespace = ".Application*";
        private static readonly string ApplicationLayerName = "Application Layer";

        private static readonly string ApiLayerNamespace = ".API*";
        private static readonly string ApiLayerName = "API Layer";

        private static readonly string InfrastructureLayerNamespace = ".Infrastructure*";
        private static readonly string InfrastructureLayerName = "Infrastructure Layer";        

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

        // Tests        

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

            // Act & Assert
            domainLayerShouldNotAccessApplicationLayer
                .And(domainLayerShouldNotAccessInfracstrutureLayer)
                .And(domainLayerShouldNotAccessApiLayer)
                .Check(Architecture);           
        }

        [Fact]
        public void ApplicationLayerShouldNotAccessApiLayer()
        {
            // Arrange
            IArchRule applicationLayerShouldNotAccessApiLayer = Types().That().Are(ApplicationLayer).Should()
                .NotDependOnAny(ApiLayer).Because(ApplicationLayerRule);

            // Act & Assert
            applicationLayerShouldNotAccessApiLayer.Check(Architecture);               
        }
    }
}