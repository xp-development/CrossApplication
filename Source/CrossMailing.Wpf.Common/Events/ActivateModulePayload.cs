using System;

namespace CrossMailing.Wpf.Common.Events
{
    public class ActivateModulePayload
    {
        public Guid ModuleGuid { get; set; }

        public ActivateModulePayload(Guid moduleGuid)
        {
            ModuleGuid = moduleGuid;
        }
    }
}