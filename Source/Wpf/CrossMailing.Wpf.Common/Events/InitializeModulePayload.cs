using System;
using CrossMailing.Common;

namespace CrossMailing.Wpf.Common.Events
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