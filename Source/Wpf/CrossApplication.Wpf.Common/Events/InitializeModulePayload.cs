using System;
using CrossApplication.Core.Common;

namespace CrossApplication.Wpf.Common.Events
{
    public class InitializeModulePayload
    {
        public Guid ModuleIdentifier { get; private set; }
        public ResourceValue ResourceValue { get; private set; }

        public InitializeModulePayload(Guid moduleIdentifier, ResourceValue resourceValue)
        {
            ModuleIdentifier = moduleIdentifier;
            ResourceValue = resourceValue;
        }
    }
}