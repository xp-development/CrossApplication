using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Net.Application.Modules
{
    public class DirectoryModuleCatalog : IModuleCatalog
    {
        public DirectoryModuleCatalog(string moduleDirectory)
        {
            _moduleDirectory = moduleDirectory;
        }

        public IEnumerable<ModuleInfo> GetModuleInfos()
        {
            var fileInfos = new DirectoryInfo(_moduleDirectory).GetFiles("*.dll");
            foreach (var fileInfo in fileInfos)
            {
                var assembly = Assembly.LoadFile(fileInfo.FullName);
                foreach (var type in assembly.GetExportedTypes())
                {
                    var customAttributes = type.GetCustomAttributes(typeof(ModuleAttribute), true);
                    if (customAttributes.Length > 0)
                    {
                        yield return new ModuleInfo { ModuleType = type, Tag = ((ModuleAttribute)customAttributes[0]).Tag };
                    }
                }
            }
        }

        private readonly string _moduleDirectory;
    }
}