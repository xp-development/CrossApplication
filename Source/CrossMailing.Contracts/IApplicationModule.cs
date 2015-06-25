using System;

namespace CrossMailing.Contracts
{
    public interface IApplicationModule
    {
        Guid ModuleIdentifier { get; } 
    }
}