using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CRMMobile.Layout
{
    public class InfiniteScolling : ListView
    {
        public InfiniteScolling() : base(ListViewCachingStrategy.RecycleElement)
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
        }

        private int outCout { get; set; }

        private void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource.Cast<object>().ToList();
            if (items.Count <= MinLength)
                return;

            if (items != null && e.Item == items[items.Count - 1])
            {
                outCout = items.Count;
                if (items.Count >= outCout)
                {
                    if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                    {
                        LoadMoreCommand.Execute(null);
                    }
                }
            }
        }

#pragma warning disable CS0618 // 'BindableProperty.Create<TDeclarer, TPropertyType>(Expression<Func<TDeclarer, TPropertyType>>, TPropertyType, BindingMode, BindableProperty.ValidateValueDelegate<TPropertyType>, BindableProperty.BindingPropertyChangedDelegate<TPropertyType>, BindableProperty.BindingPropertyChangingDelegate<TPropertyType>, BindableProperty.CoerceValueDelegate<TPropertyType>, BindableProperty.CreateDefaultValueDelegate<TDeclarer, TPropertyType>)' is obsolete: 'Create<> (generic) is obsolete as of version 2.1.0 and is no longer supported.'
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create<InfiniteScolling, ICommand>(bp => bp.LoadMoreCommand, default(ICommand));
#pragma warning restore CS0618 // 'BindableProperty.Create<TDeclarer, TPropertyType>(Expression<Func<TDeclarer, TPropertyType>>, TPropertyType, BindingMode, BindableProperty.ValidateValueDelegate<TPropertyType>, BindableProperty.BindingPropertyChangedDelegate<TPropertyType>, BindableProperty.BindingPropertyChangingDelegate<TPropertyType>, BindableProperty.CoerceValueDelegate<TPropertyType>, BindableProperty.CreateDefaultValueDelegate<TDeclarer, TPropertyType>)' is obsolete: 'Create<> (generic) is obsolete as of version 2.1.0 and is no longer supported.'

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        public static readonly BindableProperty MinLengthProperty = BindableProperty.Create("MinLength",
           typeof(int),
           typeof(InfiniteScolling),
           defaultValue: 3,
           defaultBindingMode: BindingMode.TwoWay);

        public int MinLength
        {
            get { return (int)GetValue(MinLengthProperty); }
            set { SetValue(MinLengthProperty, value); }
        }
    }
}