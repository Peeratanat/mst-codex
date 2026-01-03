using Rg.Plugins.Popup.Pages;
using System;

namespace CRMMobile.Views.Popup
{
    public partial class CalendarPopup : PopupPage
    {
        public event EventHandler<DateTime> OnSelectedDate;

        public event EventHandler<DateTime> OnClosed;

        public CalendarPopup()
        {
            InitializeComponent();
            BindingContext = this;
            //SelectedDate = DateTime.Now;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var conBackground = CalendarX.SelectedDate;
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            return base.OnBackgroundClicked();
        }

        //private DateTime selectedDate;
        //public DateTime SelectedDate
        //{
        //    get => selectedDate;
        //    set { selectedDate = value; }
        //}

        public void SetDefaultDate(DateTime dateTime)
        {
            // SelectedDate = dateTime;
            CalendarX.SelectedDate = dateTime;
        }

        public void OnOkClicked(object sender, EventArgs e)
        {
            OnClosed?.Invoke(this, CalendarX.SelectedDate);
        }
    }
}