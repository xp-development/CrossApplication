using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CrossMailing.Wpf.Common
{
    public class NavigationBarTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;
            while (!(element is NavigationBar))
            {
                element = VisualTreeHelper.GetParent(element) as FrameworkElement;
            }

            var navigationBar = ((NavigationBar) element);

            if (item != null)
                return navigationBar.BarItemTemplate ?? ((FrameworkElement) container).FindResource("BarItemTemplate") as DataTemplate;

            if (navigationBar.BarItemContextMenuTemplate != null)
                return navigationBar.BarItemContextMenuTemplate;

            return ((FrameworkElement) container).FindResource("BarItemContextMenuTemplate") as DataTemplate;
        }
    }
}