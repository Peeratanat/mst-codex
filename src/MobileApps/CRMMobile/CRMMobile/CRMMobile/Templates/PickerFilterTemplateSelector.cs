using System;
using Xamarin.Forms;

namespace CRMMobile.Templates
{
    public class PickerFilterTemplateSelector : DataTemplateSelector
    {
        private readonly Xamarin.Forms.DataTemplate Template;

        public PickerFilterTemplateSelector()
        {
            Template = new Xamarin.Forms.DataTemplate(typeof(PickerFilterDisplay));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var itemDisplayBinding = (container as PickerListiview).ItemDisplayBinding;
            var itemDisplayBinding2 = (container as PickerListiview).ItemDisplayBinding2;
            var isEnableDash = (container as PickerListiview).IsEnableDash;
            string display = string.Empty;
            if (itemDisplayBinding != null)
            {
                try
                {
                    var value = item.GetType().GetProperty(itemDisplayBinding).GetValue(item, null);
                    if (value != null)
                    {
                        display += value.ToString() + " ";
                    }
                }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
                catch (Exception)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
                {
                }
            }

            if (itemDisplayBinding2 != null)
            {
                try
                {
                    var value2 = item.GetType().GetProperty(itemDisplayBinding2).GetValue(item, null);
                    if (value2 != null)
                    {
                        if (!string.IsNullOrEmpty(display))
                        {
                            if (isEnableDash)
                                display += "-";
                            else
                                display += " ";
                            display += value2.ToString();
                        }
                        else
                            display += value2.ToString(); 
                    }
                }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
                catch (Exception e)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
                {
                }
            }
            Template.SetValue(PickerFilterDisplay.DisplayProperty, display);
            return Template;
        }
    }

    public class PickerListiview : ListView
    {
        public static readonly BindableProperty ItemDisplayBindingProperty =
          BindableProperty.Create(nameof(ItemDisplayBinding), typeof(string), typeof(PickerListiview), string.Empty, propertyChanged: OnItemDisplayBindingPropertyChanged);

        public string ItemDisplayBinding
        {
            get { return (string)GetValue(ItemDisplayBindingProperty); }
            set { SetValue(ItemDisplayBindingProperty, value); }
        }

        public static readonly BindableProperty ItemDisplayBinding2Property =
         BindableProperty.Create(nameof(ItemDisplayBinding2), typeof(string), typeof(PickerListiview), string.Empty, propertyChanged: OnItemDisplayBinding2PropertyChanged);

        public string ItemDisplayBinding2
        {
            get { return (string)GetValue(ItemDisplayBinding2Property); }
            set { SetValue(ItemDisplayBinding2Property, value); }
        }

        public bool IsEnableDash { get; set; }

        private static void OnItemDisplayBindingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as PickerListiview).ItemDisplayBinding = (string)newValue;
            }
        }

        private static void OnItemDisplayBinding2PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as PickerListiview).ItemDisplayBinding2 = (string)newValue;
            }
        }
    }

    public class PickerFilterDisplay : ViewCell
    {
        private Label label;

        public PickerFilterDisplay()
        {
            label = new Label()
            {
                Style = (Style)Application.Current.Resources["FontRegular"],
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 14
            };

            View = label;
        }

        private void SetDisplay(string display)
        {
            label.Text = display;
            Display = display;
        }

        public static readonly BindableProperty DisplayProperty =
           BindableProperty.Create("Display", typeof(string), typeof(PickerFilterDisplay), null, propertyChanged: OnParentContextPropertyChanged);

        public string Display
        {
            get { return (string)GetValue(DisplayProperty); }
            set { SetValue(DisplayProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                var _bindable = bindable as PickerFilterDisplay;
                _bindable.SetDisplay((string)newValue);
            }
        }
    }
}