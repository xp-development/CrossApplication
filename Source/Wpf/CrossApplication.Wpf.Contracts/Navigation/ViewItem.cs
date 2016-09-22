using System;
using System.Collections.Generic;

namespace CrossApplication.Wpf.Contracts.Navigation
{
    public class ViewItem
    {
        public string ViewKey { get; }
        public Type ViewType { get; }
        public bool IsAuthorizationRequired { get; }
        public string RegionName { get; }
        public IList<ViewItem> SubViewItems { get; } = new List<ViewItem>();

        public ViewItem(string viewKey, Type viewType, bool isAuthorizationRequired, string regionName)
        {
            ViewKey = viewKey;
            ViewType = viewType;
            IsAuthorizationRequired = isAuthorizationRequired;
            RegionName = regionName;
        }
    }
}