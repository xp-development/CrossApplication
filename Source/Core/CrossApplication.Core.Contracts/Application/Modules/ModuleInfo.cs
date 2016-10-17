using System;
using System.Diagnostics;

namespace CrossApplication.Core.Contracts.Application.Modules
{
    [DebuggerDisplay("ModuleType:{ModuleType} Name: {Name}")]
    public class ModuleInfo
    {
        public string Name { get; set; }
        public Type ModuleType { get; set; }
        public string Tag { get; set; } = ModuleTags.DefaultModule;
    }
}