using CRMMobile.Views.Popup;
using Rg.Plugins.Popup.Extensions;
using System;
using Xamarin.Forms;

namespace CRMMobile.Control
{
    public class SFDatePicker : BorderEntry 
    {
        private CalendarPopup calendar;
        public event EventHandler<DateTime> OnSelectedDate;

        public SFDatePicker()
        {
            calendar = new CalendarPopup();
            calendar.SetDefaultDate(DateTime.Now);
            
            Format = "dd/MM/yyy";
            Radius = 5;
            FontSize = 13.5;
            Icon = Helper.FontIcons.calendar;
            ImageAlignment = ImageAlignment.Right;
            FocusColor = Color.FromHex("#C92028");
            UnFocusColor = Color.FromHex("#8992A7");
            TextColor = Color.FromHex("#7A7A7A");
            IconColor = Color.FromHex("#8992A7");
            this.Focused += Calendar_Focused;

        }

        private async void Calendar_Focused(object sender, FocusEventArgs e)
        {
            calendar.OnClosed += async (o, e2) =>
            {
                Unfocus();
                NullableDate = e2;
                SetDisplayDate(NullableDate);
                await Prism.PrismApplicationBase.Current.MainPage.Navigation.PopPopupAsync();
            };
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Prism.PrismApplicationBase.Current.MainPage.Navigation.PushPopupAsync(calendar);
            });
            Unfocus();
        }

        private void SetDisplayDate(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                this.Placeholder = PlaceHolder;
                this.Text = null;
               
            }
            else
            {
                Text = dateTime.Value.ToString(Format);
            }
        }

        public static readonly BindableProperty FormatProperty =
            BindableProperty.Create(nameof(Format), typeof(string), typeof(SFDatePicker), null, BindingMode.TwoWay);

        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        public static readonly BindableProperty NullableDateProperty =
            BindableProperty.Create(nameof(NullableDate), typeof(DateTime?),
                typeof(SFDatePicker), null, BindingMode.TwoWay, propertyChanged: OnDateChanged);

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); }
        }

        public static readonly BindableProperty PlaceHolderProperty =
            BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(SFDatePicker), null);

        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set
            {
                SetValue(PlaceHolderProperty, value);
                Placeholder = value;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "PlaceHolder")
                Placeholder = PlaceHolder;
            if (propertyName == "FontSize")
                FontSize = FontSize;
        }

        private static void OnDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var SFDatePicker = bindable as SFDatePicker;
            var value = (DateTime?)newValue;
            SFDatePicker.SetDisplayDate(value);
            if (value == null)
                return;

            SFDatePicker.calendar.SetDefaultDate(value.Value);
        }
    }
}