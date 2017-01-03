using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace CrossApplication.Core.Contracts.Common.Navigation
{
    public interface IRegionManager
    {
        Task RequestNavigateAsync(string regionName, Uri uri);
        Task RequestNavigateAsync(string regionName, Uri uri, NavigationParameters navigationParameters);
    }
}