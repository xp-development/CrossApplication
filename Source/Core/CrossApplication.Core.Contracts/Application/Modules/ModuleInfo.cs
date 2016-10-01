using System;

namespace CrossApplication.Core.Contracts.Application.Modules
{
    public class ModuleInfo
    {
        public string Name { get; set; }
        public Type ModuleType { get; set; }
        public string Tag { get; set; } = ModuleTags.DefaultModule;
    }
}