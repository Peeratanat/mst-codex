using CRMMobile.Views.Popup;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CRMMobile.Control
{
    public class PickerFilter : BorderEntry
    {
        public static readonly BindableProperty ItemsSourceProperty =
           BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(PickerFilter), null, propertyChanged: OnItemSourcePropertyChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(PickerFilter), null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ItemBindingDisplayProperty =
            BindableProperty.Create(nameof(ItemBindingDisplay), typeof(string), typeof(PickerFilter), null);

        public static readonly BindableProperty ItemBindingDisplay2Property =
           BindableProperty.Create(nameof(ItemBindingDisplay2), typeof(string), typeof(PickerFilter), null);

        public static readonly BindableProperty TitleProperty =
           BindableProperty.Create(nameof(Title), typeof(string), typeof(PickerFilter), null, BindingMode.TwoWay, propertyChanged: OnTitleChanged);

        public static readonly BindableProperty VisibleSearchProperty =
            BindableProperty.Create(nameof(VisibleSearch), typeof(bool), typeof(PickerFilter), true, BindingMode.TwoWay, propertyChanged: OnVisibleSearchChanged);

        public static readonly BindableProperty FilterCommandProperty =
            BindableProperty.Create(nameof(FilterCommand), typeof(ICommand), typeof(PickerFilter), null);

        public static readonly BindableProperty IsEnableDashProperty =
          BindableProperty.Create(nameof(IsEnableDash), typeof(bool), typeof(PickerFilter), true);

        public static readonly BindableProperty CommandParameterProperty =
           BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(PickerFilter), null);


        public bool IsEnableDash
        {
            get { return (bool)GetValue(IsEnableDashProperty); }
            set { SetValue(IsEnableDashProperty, value); }
        }

        public ICommand FilterCommand
        {
            get { return (ICommand)GetValue(FilterCommandProperty); }
            set { SetValue(FilterCommandProperty, value); }
        }

        
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public bool VisibleSearch
        {
            get { return (bool)GetValue(VisibleSearchProperty); }
            set { SetValue(VisibleSearchProperty, value); }
        }

        public event EventHandler<TextChangedEventArgs> OnTextFilterChanged;

        public event EventHandler<Object> OnSelectItemChanged;

        public bool CanOpen { get; set; }

        public PickerFilterPopup popup;

        public PickerFilter()
        {
            CanOpen = true;
            Icon = Helper.FontIcons.expand;
            ImageAlignment = ImageAlignment.Right;
            FocusColor = Color.FromHex("#C92028");
            UnFocusColor = Color.FromHex("#8992A7");
            TextColor = Color.FromHex("#7A7A7A");
            IconColor = Color.FromHex("#8992A7");
            FontSize = 13.5;
            Focused += PickerFilter_Focused;
            popup = new PickerFilterPopup(null);
            popup.SelectedItemChanged += Popup_SelectedItemChanged;
        }

        private void PickerFilter_Focused(object sender, FocusEventArgs e)
        {
            Unfocus();
            Open();
        }

        public async void Open()
        {
            if (!CanOpen)
                return;

            popup.OnFilterTextChanged += (o, e1) =>
            {
                OnTextFilterChanged?.Invoke(this, e1);
                if (FilterCommand == null)
                    return;

                if (FilterCommand.CanExecute(e1.NewTextValue))
                    FilterCommand.Execute(e1.NewTextValue);
            };

            popup.OnClosed += (o, e) =>
            {
                Unfocus();
            };
            await Navigation.PushPopupAsync(popup);
        }

        private async void Popup_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PopPopupAsync();
            SetSelectedItem(e.SelectedItem);
            Unfocus();
        }

        private void SetDisplay(string text)
        {
            Text = text;
        }

        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public string ItemBindingDisplay
        {
            get { return (string)GetValue(ItemBindingDisplayProperty); }
            set { SetValue(ItemBindingDisplayProperty, value); }
        }

        public string ItemBindingDisplay2
        {
            get { return (string)GetValue(ItemBindingDisplay2Property); }
            set { SetValue(ItemBindingDisplay2Property, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private void SetSelectedItem(object obj)
        {
            SelectedItem = obj;
            OnSelectItemChanged?.Invoke(this, obj);

            if (obj == null)
            {
                SetDisplay(string.Empty);
                return;
            }

            string dispaly = string.Empty;
            if (!string.IsNullOrEmpty(ItemBindingDisplay))
            {
                dispaly += (string)obj.GetType().GetProperty(ItemBindingDisplay).GetValue(obj, null);
            }
            if (!string.IsNullOrEmpty(ItemBindingDisplay2))
            {
                dispaly += IsEnableDash ? "-" : " ";
                dispaly += (string)obj.GetType().GetProperty(ItemBindingDisplay2).GetValue(obj, null);
            }

            if (!string.IsNullOrEmpty(dispaly))
                SetDisplay(dispaly);
            else
                SetDisplay(string.Empty);
        }

        public static void OnItemSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var pickerfiler = (bindable as PickerFilter);
            pickerfiler.popup.SetItemSource(pickerfiler.ItemsSource);
            pickerfiler.popup.SetItemDisplayBinding(pickerfiler.ItemBindingDisplay);
            pickerfiler.popup.SetItemDisplayBinding2(pickerfiler.ItemBindingDisplay2);
            pickerfiler.popup.SetIsEnableDash(pickerfiler.IsEnableDash);
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as PickerFilter).SetSelectedItem(newValue);
        }

        private static void OnVisibleSearchChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as PickerFilter).popup.VisibleSearch((bool)newValue);
        }

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as PickerFilter).popup.SetTitle((string)newValue);
        }
    }
}