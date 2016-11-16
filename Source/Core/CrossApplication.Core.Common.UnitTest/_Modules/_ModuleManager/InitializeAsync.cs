using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Modules;
using CrossApplication.Core.Common.UnitTest._Modules.TestClasses;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using FluentAssertions;
using Moq;
using Xunit;

namespace CrossApplication.Core.Common.UnitTest._Modules._ModuleManager
{
    public class InitializeAsync
    {
        [Fact]
        public async void ShouldInitializeAllModules()
        {
            var moduleCatalogMock = new Mock<IModuleCatalog>();
            moduleCatalogMock.Setup(x => x.GetModuleInfosAsync()).Returns(Task.FromResult<IEnumerable<ModuleInfo>>(new List<ModuleInfo>
            {
                new ModuleInfo {Name = "ModuleA", ModuleType = typeof(ModuleA)},
                new ModuleInfo {Name = "ModuleB", ModuleType = typeof(ModuleB), Tag = ModuleTags.Infrastructure},
                new ModuleInfo {Name = "ModuleC", ModuleType = typeof(ModuleC)}
            }));

            var unityContainerMock = new Mock<IContainer>();
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleA))).Returns(new ModuleA());
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleB))).Returns(new ModuleB());
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleC))).Returns(new ModuleC());
            var moduleManager = new ModuleManager(unityContainerMock.Object, moduleCatalogMock.Object);

            await moduleManager.InizializeAsync();

            unityContainerMock.Verify(x => x.Resolve(typeof(ModuleA)));
            unityContainerMock.Verify(x => x.Resolve(typeof(ModuleB)));
            unityContainerMock.Verify(x => x.Resolve(typeof(ModuleC)));
        }

        [Fact]
        public async void ShouldInitializeAllModulesWithDefaultTagModule()
        {
            var moduleCatalogMock = new Mock<IModuleCatalog>();
            moduleCatalogMock.Setup(x => x.GetModuleInfosAsync()).Returns(Task.FromResult<IEnumerable<ModuleInfo>>(new List<ModuleInfo>
            {
                new ModuleInfo {Name = "ModuleA", ModuleType = typeof(ModuleA)},
                new ModuleInfo {Name = "ModuleB", ModuleType = typeof(ModuleB), Tag = "NotLoadedModule"},
                new ModuleInfo {Name = "ModuleC", ModuleType = typeof(ModuleC)}
            }));

            var unityContainerMock = new Mock<IContainer>();
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleA))).Returns(new ModuleA());
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleC))).Returns(new ModuleC());
            var moduleManager = new ModuleManager(unityContainerMock.Object, moduleCatalogMock.Object);

            await moduleManager.InizializeAsync(ModuleTags.DefaultModule);

            unityContainerMock.Verify(x => x.Resolve(typeof(ModuleA)));
            unityContainerMock.Verify(x => x.Resolve(typeof(ModuleC)));
        }

        [Fact]
        public async void ShouldInitializeAllModulesWithDefaultTagInfrastructure()
        {
            var moduleCatalogMock = new Mock<IModuleCatalog>();
            moduleCatalogMock.Setup(x => x.GetModuleInfosAsync()).Returns(Task.FromResult<IEnumerable<ModuleInfo>>(new List<ModuleInfo>
            {
                new ModuleInfo {Name = "ModuleA", ModuleType = typeof(ModuleA), Tag = "Infrastructure"},
                new ModuleInfo {Name = "ModuleB", ModuleType = typeof(ModuleB)},
                new ModuleInfo {Name = "ModuleC", ModuleType = typeof(ModuleC), Tag = "Infrastructure"}
            }));

            var unityContainerMock = new Mock<IContainer>();
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleA))).Returns(new ModuleA());
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleC))).Returns(new ModuleC());
            var moduleManager = new ModuleManager(unityContainerMock.Object, moduleCatalogMock.Object);

            await moduleManager.InizializeAsync("Infrastructure");

            unityContainerMock.Verify(x => x.Resolve(typeof(ModuleA)));
            unityContainerMock.Verify(x => x.Resolve(typeof(ModuleC)));
        }

        [Fact]
        public void ShouldThrowExceptionIfModuleNotInheritsFromIModule()
        {
            var moduleCatalogMock = new Mock<IModuleCatalog>();
            moduleCatalogMock.Setup(x => x.GetModuleInfosAsync()).Returns(Task.FromResult<IEnumerable<ModuleInfo>>(new List<ModuleInfo> { new ModuleInfo {Name = "ModuleWithoutIModule", ModuleType = typeof(ModuleWithoutIModule)}}));
            var unityContainerMock = new Mock<IContainer>();
            unityContainerMock.Setup(x => x.Resolve(typeof(ModuleWithoutIModule))).Returns(new ModuleWithoutIModule());
            var moduleManager = new ModuleManager(unityContainerMock.Object, moduleCatalogMock.Object);

            new Action(() => moduleManager.InizializeAsync().Wait())
                .ShouldThrow<ArgumentException>().WithMessage("CrossApplication.Core.Common.UnitTest._Modules.TestClasses.ModuleWithoutIModule does not inherit from IModule.");
        }
    }
}