using System;

namespace CrossApplication.Core.Contracts.Application.Modules
{
    public class ModuleAttribute : Attribute
    {
        public string Tag { get; set; } = ModuleTags.DefaultModule;
    }
}
