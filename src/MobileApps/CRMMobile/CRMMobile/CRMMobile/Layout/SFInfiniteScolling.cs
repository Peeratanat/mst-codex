using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace CRMMobile.Layout
{
    public class SFInfiniteScolling : ScrollView
    {
        private double y = 0;
        private SFInfinieView view;
        private double lastedpoint;

        public SFInfiniteScolling()
        {
            view = new SFInfinieView();
            Scrolled += SFInfiniteScolling_Scrolled;
            Content = view;
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
           nameof(ItemTemplate),
           typeof(DataTemplate),
           typeof(SFInfiniteScolling),
           default(DataTemplate));

        public static readonly BindableProperty EmptyTemplateProperty = BindableProperty.Create(
           nameof(EmtyTemplate),
           typeof(DataTemplate),
           typeof(SFInfiniteScolling),
           default(DataTemplate));

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(SFInfiniteScolling),
            null,
            BindingMode.TwoWay,
            propertyChanged: ItemsChanged);

        //public static readonly BindableProperty EnableEmptyViewProperty = BindableProperty.Create(
        //  nameof(EnableEmptyView),
        //  typeof(bool),
        //  typeof(SFInfiniteScolling),
        //  false);

#pragma warning disable CS0618 // 'BindableProperty.Create<TDeclarer, TPropertyType>(Expression<Func<TDeclarer, TPropertyType>>, TPropertyType, BindingMode, BindableProperty.ValidateValueDelegate<TPropertyType>, BindableProperty.BindingPropertyChangedDelegate<TPropertyType>, BindableProperty.BindingPropertyChangingDelegate<TPropertyType>, BindableProperty.CoerceValueDelegate<TPropertyType>, BindableProperty.CreateDefaultValueDelegate<TDeclarer, TPropertyType>)' is obsolete: 'Create<> (generic) is obsolete as of version 2.1.0 and is no longer supported.'
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create<InfiniteScolling, ICommand>(bp => bp.LoadMoreCommand, default(ICommand));
#pragma warning restore CS0618 // 'BindableProperty.Create<TDeclarer, TPropertyType>(Expression<Func<TDeclarer, TPropertyType>>, TPropertyType, BindingMode, BindableProperty.ValidateValueDelegate<TPropertyType>, BindableProperty.BindingPropertyChangedDelegate<TPropertyType>, BindableProperty.BindingPropertyChangingDelegate<TPropertyType>, BindableProperty.CoerceValueDelegate<TPropertyType>, BindableProperty.CreateDefaultValueDelegate<TDeclarer, TPropertyType>)' is obsolete: 'Create<> (generic) is obsolete as of version 2.1.0 and is no longer supported.'

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        public static readonly BindableProperty MinLengthProperty = BindableProperty.Create(nameof(MinLength),
           typeof(int),
           typeof(InfiniteScolling),
           defaultValue: 3,
           defaultBindingMode: BindingMode.TwoWay);

        public int MinLength
        {
            get { return (int)GetValue(MinLengthProperty); }
            set { SetValue(MinLengthProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public DataTemplate EmtyTemplate
        {
            get => (DataTemplate)GetValue(EmptyTemplateProperty);
            set => SetValue(EmptyTemplateProperty, value);
        }

        //public bool EnableEmptyView
        //{
        //    get => (bool)GetValue(EnableEmptyViewProperty);
        //    set => SetValue(EnableEmptyViewProperty, value);
        //}

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as SFInfiniteScolling;
            if (control == null)
                return;
        }

        private async void SFInfiniteScolling_Scrolled(object sender, ScrolledEventArgs e)
        {
            var item = sender as ScrollView;
            if (item == null)
                return;

            if (Device.RuntimePlatform == Device.iOS)
            {
                if (Math.Abs(e.ScrollY) < Double.Epsilon && y > 15)
                {
                    await item.ScrollToAsync(0, y, false);
                    return;
                }

                if (Math.Abs(item.ScrollY) > Double.Epsilon)
                    y = item.ScrollY;
            }

            var scrollingSpace = item.ContentSize.Height - item.Height;

            if ((int)scrollingSpace <= (int)e.ScrollY)
            {
                if (Math.Abs(lastedpoint) < Math.Abs(scrollingSpace))
                {
                    lastedpoint = scrollingSpace;
                    if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                    {
                        LoadMoreCommand.Execute(null);
                    }
                }
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == SFInfiniteScolling.ItemsSourceProperty.PropertyName)
            {
                //if(ItemsSource == null)
                //{
                //    view.AddEmtyView(EmtyTemplate);
                //}
                if (ItemsSource != null && ItemsSource is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged -= Collection_CollectionChanged;
                    collection.CollectionChanged += Collection_CollectionChanged;
                }
            }
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    view.AddView(item, ItemTemplate);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                view.UpdateView(e.NewStartingIndex, e.NewItems[e.NewStartingIndex], ItemTemplate);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (Children.Count > 0)
                {
                    view.RemoveView(e.OldStartingIndex);
                }

                var itemSourceCount = ItemsSource.Cast<object>().Count();
                if (itemSourceCount == 0)
                    AddEmptyView();
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                var itemSourceCount = ItemsSource.Cast<object>().Count();
                if (itemSourceCount == 0)
                    AddEmptyView();
            }
            
        }

        private void AddEmptyView()
        {
            view.RemoveAllView();
            view.AddEmtyView(EmtyTemplate);
        }
    }
}