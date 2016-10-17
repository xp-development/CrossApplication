using System.IO;
using System.Linq;
using CrossApplication.Core.Net.Application.Modules;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Core.Net.Application.UnitTest._Modules._DirectoryModuleCatalog
{
    public class GetModuleInfos
    {
        public GetModuleInfos()
        {
            if (Directory.Exists(_moduleDirectoryPath))
            {
                Directory.Delete(_moduleDirectoryPath, true);
            }

            Directory.CreateDirectory(_moduleDirectoryPath);
            File.Copy(_moduleAssemblyName, Path.Combine(_moduleDirectoryPath, _moduleAssemblyName));
        }

        [Fact]
        public async void Usage()
        {
            var catalog = new DirectoryModuleCatalog("./Modules");

            var moduleInfos = (await catalog.GetModuleInfosAsync()).ToArray();

            moduleInfos.Length.Should().Be(2);
        }

        private readonly string _moduleAssemblyName = "CrossApplication.Core.Net.Application.UnitTest.dll";
        private readonly string _moduleDirectoryPath = "./Modules";
    }
}