using System.Collections.Specialized;
using Fluent;
using Prism.Regions;

namespace CrossMailing.Wpf.Common.RegionAdapters
{
    public class RibbonRegionAdapter : RegionAdapterBase<Ribbon>
    {
        public RibbonRegionAdapter(IRegionBehaviorFactory factory)
            : base(factory)
        {
        }

        protected override void Adapt(IRegion region, Ribbon regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var element in e.NewItems)
                    {
                        regionTarget.Tabs.Add((RibbonTabItem) element);
                    }
                }

                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var element in e.OldItems)
                    {
                        regionTarget.Tabs.Remove((RibbonTabItem) element);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}