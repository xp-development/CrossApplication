using CrossMailing.Common;
using Prism.Mvvm;

namespace CrossMailing.Wpf.Common.Navigation
{
    public class NavigationItem : BindableBase
    {
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                OnPropertyChanged();
            }
        }

        public NavigationItem(ResourceValue resourceValue)
        {
            Label = LocalizationManager.Get(resourceValue);
        }

        private string _label;
    }
}