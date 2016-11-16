using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CrossApplication.Wpf.Themes.Controls
{
    public class NavigationBar : Control, INotifyPropertyChanged
    {
        public int MaximumCountVisibleElements
        {
            get { return (int) GetValue(MaximumCountVisibleElementsProperty); }
            set { SetValue(MaximumCountVisibleElementsProperty, value); }
        }

        public DataTemplate BarItemContextMenuTemplate
        {
            get { return (DataTemplate) GetValue(BarItemContextMenuTemplateProperty); }
            set { SetValue(BarItemContextMenuTemplateProperty, value); }
        }

        public DataTemplate BarItemTemplate
        {
            get { return (DataTemplate) GetValue(BarItemTemplateProperty); }
            set { SetValue(BarItemTemplateProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable) GetValue(ItemsSourceProperty); }
            set
            {
                if (value == null)
                    ClearValue(ItemsControl.ItemsSourceProperty);
                else
                    SetValue(ItemsControl.ItemsSourceProperty, value);
            }
        }

        public Style ItemContainerStyle
        {
            get { return (Style) GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        public Orientation BarItemsOrientation
        {
            get { return (Orientation) GetValue(BarItemsOrientationProperty); }
            set { SetValue(BarItemsOrientationProperty, value); }
        }

        public IEnumerable BarItems
        {
            get
            {
                if (_barItems == null)
                    CreateItemCollection();

                if (_barItems == null || _barItems.Count == 0)
                    yield break;

                var min = Math.Min(_barItems.Count, MaximumCountVisibleElements);
                for (var i = 0; i < min; ++i)
                    yield return _barItems.GetItemAt(i);

                if (_barItems.Count > MaximumCountVisibleElements)
                    yield return null;
            }
        }

        public IEnumerable ContextItems
        {
            get
            {
                if (_barItems == null)
                    CreateItemCollection();

                if (_barItems == null)
                    yield break;

                for (var i = MaximumCountVisibleElements; i < _barItems.Count; ++i)
                    yield return _barItems.GetItemAt(i);
            }
        }

        static NavigationBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationBar), new FrameworkPropertyMetadata(typeof(NavigationBar)));
        }

        private void CreateItemCollection()
        {
            if (ItemsSource == null)
                return;

            _barItems = new CollectionView(ItemsSource);
        }

        public override void OnApplyTemplate()
        {
        }

        private static void OnItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ((NavigationBar) dependencyObject).OnItemsSourceChanged(eventArgs);
        }

        private void OnItemsSourceChanged(DependencyPropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.OldValue is INotifyCollectionChanged)
            {
                var collection = (INotifyCollectionChanged) eventArgs.OldValue;
                collection.CollectionChanged -= OnCollectionChanged;
            }

            if (eventArgs.NewValue is INotifyCollectionChanged)
            {
                var collection = (INotifyCollectionChanged) eventArgs.NewValue;
                collection.CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            OnPropertyChanged("BarItems");
            OnPropertyChanged("ContextItems");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty ItemContainerStyleProperty = DependencyProperty.Register(
            "ItemContainerStyle", typeof(Style), typeof(NavigationBar), new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty BarItemContextMenuTemplateProperty = DependencyProperty.Register(
            "BarItemContextMenuTemplate", typeof(DataTemplate), typeof(NavigationBar), new PropertyMetadata(null));

        public static readonly DependencyProperty BarItemsOrientationProperty = DependencyProperty.Register(
            "BarItemsOrientation", typeof(Orientation), typeof(NavigationBar), new PropertyMetadata(Orientation.Horizontal));

        public static readonly DependencyProperty BarItemTemplateProperty = DependencyProperty.Register(
            "BarItemTemplate", typeof(DataTemplate), typeof(NavigationBar), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(NavigationBar),
            new FrameworkPropertyMetadata(null, OnItemsSourceChanged));

        public static readonly DependencyProperty MaximumCountVisibleElementsProperty = DependencyProperty.Register("MaximumCountVisibleElements", typeof(int),
            typeof(NavigationBar), new PropertyMetadata(default(int)));

        private CollectionView _barItems;
    }
}