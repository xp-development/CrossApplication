using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Net.Application.Modules
{
    public class DirectoryModuleCatalog : IModuleCatalog
    {
        public DirectoryModuleCatalog(string moduleDirectory)
        {
            _moduleDirectory = moduleDirectory;
        }

        public async Task<IEnumerable<ModuleInfo>> GetModuleInfosAsync()
        {
            if (_moduleInfos.Count > 0)
            {
                return await Task.FromResult<IEnumerable<ModuleInfo>>(_moduleInfos);
            }

            _moduleInfos.AddRange(await Task.Run(() =>
            {
                var moduleInfos = new List<ModuleInfo>();
                foreach (var fileInfo in new DirectoryInfo(_moduleDirectory).GetFiles("*.dll"))
                {
                    var assembly = Assembly.LoadFrom(fileInfo.FullName);
                    foreach (var moduleType in assembly.GetExportedTypes())
                    {
                        var customAttributes = moduleType.GetCustomAttributes(typeof(ModuleAttribute), true);
                        if (customAttributes.Length > 0)
                        {
                            moduleInfos.Add(new ModuleInfo {ModuleType = moduleType, Tag = ((ModuleAttribute) customAttributes[0]).Tag});
                        }
                    }
                }

                return moduleInfos;
            }));

            return _moduleInfos;
        }

        private readonly string _moduleDirectory;
        private readonly List<ModuleInfo> _moduleInfos = new List<ModuleInfo>();
    }
}