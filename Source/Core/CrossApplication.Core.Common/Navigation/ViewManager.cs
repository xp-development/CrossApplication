using System;
using System.Collections.Generic;
using System.Linq;
using CrossApplication.Core.Contracts.Navigation;

namespace CrossApplication.Core.Common.Navigation
{
    public class ViewManager : IViewManager
    {
        public ViewItem LoginViewItem { get; set; }

        public void RegisterRichShell(string name, ViewItem viewItem)
        {
            _richShells.Add(name, viewItem);
        }

        public ViewItem GetRichShell(string name)
        {
            return _richShells[name];
        }

        public void AddViewItem(ViewItem viewItem)
        {
            if (_viewItems.Any(x => x.ViewKey == viewItem.ViewKey))
                throw new ArgumentException("ViewKey is already registered.", nameof(viewItem));

            _viewItems.Add(viewItem);
        }

        public ViewItem GetViewItem(string viewKey)
        {
            var viewItem = _viewItems.FirstOrDefault(x => x.ViewKey == viewKey);
            if (viewItem == null)
            {
                throw new ArgumentException("ViewKey was not added.", nameof(viewKey));
            }

            return viewItem;
        }

        private readonly List<ViewItem> _viewItems = new List<ViewItem>();
        private readonly Dictionary<string, ViewItem> _richShells = new Dictionary<string, ViewItem>();
    }
}