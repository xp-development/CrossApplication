using System.Collections.Generic;
using CrossApplication.Core.Application.Modules;
using CrossApplication.Core.Application.UnitTest._Modules.TestClasses;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using FluentAssertions;
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

            var unityContainerMock = new Mock<IContainer>();
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleA))).Returns(moduleA);
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleB))).Returns(moduleB);
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleC))).Returns(moduleC);
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

            var unityContainerMock = new Mock<IContainer>();
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleB))).Returns(moduleB);
            var moduleManager = new ModuleManager(unityContainerMock.Object);
            moduleManager.SetModuleCatalog(moduleCatalogMock.Object);
            await moduleManager.InizializeAsync(ModuleTags.Infrastructure);

            await moduleManager.ActivateAsync(ModuleTags.Infrastructure);

            moduleB.IsActivated.Should().BeTrue();
        }
    }
}