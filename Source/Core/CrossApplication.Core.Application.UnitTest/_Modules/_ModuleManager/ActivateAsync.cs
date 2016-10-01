using System;
using System.Collections.Generic;
using CrossApplication.Core.Application.Modules;
using CrossApplication.Core.Application.UnitTest._Modules.TestClasses;
using CrossApplication.Core.Contracts.Application.Modules;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Moq;
using Xunit;

namespace CrossApplication.Core.Application.UnitTest._Modules._ModuleManager
{
    public class ActivateAsync
    {
        [Fact]
        public async void ShouldActivateAllInitializedModules()
        {
            var moduleCatalogMock = new Mock<IModuleCatalog>();
            moduleCatalogMock.Setup(x => x.GetModuleInfos()).Returns(new List<ModuleInfo>
            {
                new ModuleInfo {Name = "ModuleA", ModuleType = typeof(ModuleA)},
                new ModuleInfo {Name = "ModuleB", ModuleType = typeof(ModuleB)},
                new ModuleInfo {Name = "ModuleC", ModuleType = typeof(ModuleC)}
            });
            var moduleA = new ModuleA();
            var moduleB = new ModuleB();
            var moduleC = new ModuleC();

            var unityContainerMock = new Mock<IUnityContainer>();
            unityContainerMock.Setup(x => x.Resolve(It.Is<Type>(y => y == typeof(ModuleA)), It.IsAny<string>(), It.IsAny<ResolverOverride[]>())).Returns(moduleA);
            unityContainerMock.Setup(x => x.Resolve(It.Is<Type>(y => y == typeof(ModuleB)), It.IsAny<string>(), It.IsAny<ResolverOverride[]>())).Returns(moduleB);
            unityContainerMock.Setup(x => x.Resolve(It.Is<Type>(y => y == typeof(ModuleC)), It.IsAny<string>(), It.IsAny<ResolverOverride[]>())).Returns(moduleC);
            var moduleManager = new ModuleManager(unityContainerMock.Object);
            moduleManager.SetModuleCatalog(moduleCatalogMock.Object);
            await moduleManager.InizializeAsync();

            await moduleManager.ActivateAsync();

            moduleA.IsActivated.Should().BeTrue();
            moduleB.IsActivated.Should().BeTrue();
            moduleC.IsActivated.Should().BeTrue();
        }

        [Fact]
        public async void ShouldActivateInitializedModulesWithTagInfrastructure()
        {
            var moduleCatalogMock = new Mock<IModuleCatalog>();
            moduleCatalogMock.Setup(x => x.GetModuleInfos()).Returns(new List<ModuleInfo>
            {
                new ModuleInfo {Name = "ModuleA", ModuleType = typeof(ModuleA)},
                new ModuleInfo {Name = "ModuleB", ModuleType = typeof(ModuleB), Tag = ModuleTags.Infrastructure },
                new ModuleInfo {Name = "ModuleC", ModuleType = typeof(ModuleC)}
            });
            var moduleB = new ModuleB();

            var unityContainerMock = new Mock<IUnityContainer>();
            unityContainerMock.Setup(x => x.Resolve(It.Is<Type>(y => y == typeof(ModuleB)), It.IsAny<string>(), It.IsAny<ResolverOverride[]>())).Returns(moduleB);
            var moduleManager = new ModuleManager(unityContainerMock.Object);
            moduleManager.SetModuleCatalog(moduleCatalogMock.Object);
            await moduleManager.InizializeAsync(ModuleTags.Infrastructure);

            await moduleManager.ActivateAsync(ModuleTags.Infrastructure);

            moduleB.IsActivated.Should().BeTrue();
        }
    }
}