using System.Linq;
using CrossApplication.Core.Common.Modules;
using CrossApplication.Core.Common.UnitTest._Modules.TestClasses;
using CrossApplication.Core.Contracts.Application.Modules;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Core.Common.UnitTest._Modules._ModuleCatalog
{
    public class GetModuleInfos
    {
        [Fact]
        public async void ShouldReturnAddedModuleInfos()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModuleInfo(new ModuleInfo {ModuleType = typeof(ModuleA), Name = "ModuleA"});
            catalog.AddModuleInfo(new ModuleInfo {ModuleType = typeof(ModuleB), Name = "ModuleB"});
            catalog.AddModuleInfo(new ModuleInfo {ModuleType = typeof(ModuleC), Name = "ModuleC"});

            var moduleInfos = (await catalog.GetModuleInfosAsync()).ToArray();

            moduleInfos.Length.Should().Be(3);
            moduleInfos[0].Name.Should().Be("ModuleA");
            moduleInfos[0].ModuleType.Should().Be(typeof(ModuleA));
            moduleInfos[1].Name.Should().Be("ModuleB");
            moduleInfos[1].ModuleType.Should().Be(typeof(ModuleB));
            moduleInfos[2].Name.Should().Be("ModuleC");
            moduleInfos[2].ModuleType.Should().Be(typeof(ModuleC));
        }
    }
}