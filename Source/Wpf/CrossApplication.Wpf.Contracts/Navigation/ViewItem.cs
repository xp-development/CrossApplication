using System.Collections.Generic;

namespace CrossApplication.Wpf.Contracts.Navigation
{
    public class ViewItem
    {
        public string ViewKey { get; }
        public bool IsAuthorizationRequired { get; }
        public string RegionName { get; }
        public IList<ViewItem> SubViewItems { get; } = new List<ViewItem>();

        public ViewItem(string viewKey, string regionName, bool isAuthorizationRequired = false)
        {
            ViewKey = viewKey;
            RegionName = regionName;
            IsAuthorizationRequired = isAuthorizationRequired;
        }
    }
}