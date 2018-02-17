using System.Collections.Generic;

namespace CrossApplication.Core.Contracts.Navigation
{
    [System.Diagnostics.DebuggerDisplay("{ViewKey}->{RegionName}")]
    public class ViewItem
    {
        public string RichShellName { get; }
        public string ViewKey { get; }
        public bool IsAuthorizationRequired { get; }
        public string RegionName { get; }
        public IList<ViewItem> SubViewItems { get; } = new List<ViewItem>();

        public ViewItem(string viewKey, string regionName, bool isAuthorizationRequired = false, string richShellName = null)
        {
            ViewKey = viewKey;
            RegionName = regionName;
            IsAuthorizationRequired = isAuthorizationRequired;
            RichShellName = richShellName;
        }
    }
}