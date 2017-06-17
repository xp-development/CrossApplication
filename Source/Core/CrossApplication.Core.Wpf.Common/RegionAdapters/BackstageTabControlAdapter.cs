using System.Collections.Specialized;
using Fluent;
using Prism.Regions;

namespace CrossApplication.Core.Wpf.Common.RegionAdapters
{
    public class BackstageTabControlAdapter : RegionAdapterBase<BackstageTabControl>
    {
        public BackstageTabControlAdapter(IRegionBehaviorFactory factory)
            : base(factory)
        {
        }

        protected override void Adapt(IRegion region, BackstageTabControl regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var element in e.NewItems)
                    {
                        regionTarget.Items.Add((RibbonTabItem) element);
                    }
                }

                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var element in e.OldItems)
                    {
                        regionTarget.Items.Remove((RibbonTabItem) element);
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