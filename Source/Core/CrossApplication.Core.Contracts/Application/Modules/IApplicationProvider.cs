using System;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Common.Navigation;

namespace CrossApplication.Core.Contracts.Application.Modules
{
    public interface IApplicationProvider
    {
        void SetRichShell(object richShell);
        object CreateView(Uri uri);
        Task ActivateViewAsync(object view, NavigationParameters navigationParameters);
        Task DeactivateActiveViewAsync();
    }
}