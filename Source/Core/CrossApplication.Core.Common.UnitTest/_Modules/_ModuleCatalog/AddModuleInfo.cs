using System;
using CrossApplication.Core.Common.Modules;
using CrossApplication.Core.Common.UnitTest._Modules.TestClasses;
using CrossApplication.Core.Contracts.Application.Modules;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Core.Common.UnitTest._Modules._ModuleCatalog
{
    public class AddModuleInfo
    {
        [Fact]
        public void ShouldThrowExceptionIfModuleTypeIsNotSet()
        {
            var catalog = new ModuleCatalog();

            new Action(() => catalog.AddModuleInfo(new ModuleInfo()))
                .ShouldThrow<ArgumentException>().WithMessage("ModuleType is not set.");
        }

        [Fact]
        public void ShouldThrowExceptionIfModuleTypeWasAlreadyAdded()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModuleInfo(new ModuleInfo {ModuleType = typeof(ModuleA)});

            new Action(() => catalog.AddModuleInfo(new ModuleInfo {ModuleType = typeof(ModuleA)}))
                .ShouldThrow<ArgumentException>().WithMessage($"ModuleType {typeof(ModuleA)} was already added.");
        }
    }
}