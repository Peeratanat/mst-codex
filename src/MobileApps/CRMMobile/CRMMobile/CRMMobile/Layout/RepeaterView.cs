using System.Collections;
using Xamarin.Forms;

namespace CRMMobile.Layout
{
    public class RepeaterView : StackLayout
    {
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(RepeaterView),
            default(DataTemplate));

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(object),
            typeof(RepeaterView),
            null,
            BindingMode.TwoWay,
            propertyChanged: ItemsChanged);

        public RepeaterView()
        {
            Spacing = 0;
            Padding = 0;
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

        protected virtual View ViewFor(object item)
        {
            View view = null;

            if (ItemTemplate != null)
            {
                var content = ItemTemplate.CreateContent();
                view = content is View ? content as View : ((ViewCell)content).View;
                view.BindingContext = item;
            }

            return view;
        }

        public void UpdateLayout(IEnumerable items)
        {
            Children.Clear();
            if (items == null) return;

            if (Orientation == StackOrientation.Horizontal)
            {
                foreach (var item in items)
                {
                    Children.Add(ViewFor(item));
                }
            }
            else
            {
                var grid = new Grid();
                var index = 0;
                var indexColumn = 0;
                bool layoutSide = false; // false is left , true is right

                foreach (var item in items)
                {
                    grid.RowSpacing = 2;
                    indexColumn++;

                    if (!layoutSide)
                    {
                        //Definition row when new line
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(45) });
                        //Left
                        layoutSide = true;
                        var leftBox = new BoxView { Color = Color.Red };
                        grid.Children.Add(ViewFor(item), 0, index);
                    }
                    else
                    {
                        //Right
                        layoutSide = false;
                        var rightBox = new BoxView { Color = Color.DarkOrchid };
                        grid.Children.Add(ViewFor(item), 1, index);

                        //Next Position
                        index++;
                    }
                }

                Children.Add(grid);
            }
        }

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as RepeaterView;
            if (control == null)
                return;

            IEnumerable items = (IEnumerable)newValue;
            control.UpdateLayout(items);
        }
    }
}