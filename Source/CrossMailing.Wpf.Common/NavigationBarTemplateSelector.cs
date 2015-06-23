using System.Windows;
using System.Windows.Controls;

namespace CrossMailing.Wpf.Common
{
    public class NavigationBarTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return ((FrameworkElement)container).FindResource("BarItemContextMenuTemplate") as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}