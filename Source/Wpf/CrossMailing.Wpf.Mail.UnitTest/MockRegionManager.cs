using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Prism.Regions;

namespace CrossMailing.Wpf.Mail.UnitTest
{
    public class MockRegionManager : IRegionManager
    {
        private readonly MockRegionCollection _regions = new MockRegionCollection();

        public IRegionCollection Regions
        {
            get { return _regions; }
        }

        public IRegion AttachNewRegion(object regionTarget, string regionName)
        {
            throw new NotImplementedException();
        }

        public IRegionManager CreateRegionManager()
        {
            throw new NotImplementedException();
        }

        public bool Navigate(Uri source)
        {
            throw new NotImplementedException();
        }
    }

    internal class MockRegionCollection : IRegionCollection
    {
        private readonly Dictionary<string, IRegion> _regions = new Dictionary<string, IRegion>();

        public IEnumerator<IRegion> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IRegion this[string regionName]
        {
            get { return _regions[regionName]; }
        }

        public void Add(IRegion region)
        {
            _regions[region.Name] = region;
        }

        public bool Remove(string regionName)
        {
            throw new NotImplementedException();
        }

        public bool ContainsRegionWithName(string regionName)
        {
            return _regions.ContainsKey(regionName);
        }

        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;
    }

    public class MockRegion : IRegion
    {
        public List<object> AddedViews = new List<object>();

        public string Name { get; set; }

        public IRegionManager Add(object view)
        {
            AddedViews.Add(view);
            return null;
        }

        public void Remove(object view)
        {
            AddedViews.Remove(view);
        }

        public IViewsCollection Views
        {
            get { return new MockViewsCollection(AddedViews); }
        }

        public void Activate(object view)
        {
            SelectedItem = view;
        }

        public void Deactivate(object view)
        {
            throw new NotImplementedException();
        }

        public IRegionManager Add(object view, string viewName)
        {
            Add(view);
            return null;
        }

        public object GetView(string viewName)
        {
            return AddedViews.Count > 0 ? AddedViews[0] : null;
        }

        public IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
        {
            throw new NotImplementedException();
        }

        public IRegionManager RegionManager
        {
            get { throw new NotImplementedException(); }         
            set { throw new NotImplementedException(); }
        }

        public IRegionBehaviorCollection Behaviors
        {
            get { throw new NotImplementedException(); }
        }

        public object SelectedItem { get; set; }

        public IViewsCollection ActiveViews
        {
            get { throw new NotImplementedException(); }
        }

        public object Context
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public NavigationParameters NavigationParameters
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RequestNavigate(Uri source, Action<NavigationResult> navigationCallback)
        {
            AddedViews.Add(source);
        }

        public void RequestNavigate(Uri source, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            throw new NotImplementedException();
        }

        public IRegionNavigationService NavigationService
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        public Comparison<object> SortComparison
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void MoveFrom(IRegion sourceRegion, object view)
        {
            throw new NotImplementedException();
        }

        public void MoveFrom(IRegion sourceRegion, object view, string viewName)
        {
            throw new NotImplementedException();
        }
    }

    internal class MockViewsCollection : IViewsCollection
    {
        private readonly IList<object> _views;

        public MockViewsCollection(IList<object> views)
        {
            _views = views;
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<object> GetEnumerator()
        {
            return _views.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
