using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CRMMobile.Views.Popup
{
    public partial class PickerFilterPopup : PopupPage
    {
        public event EventHandler<TextChangedEventArgs> OnFilterTextChanged;

        public event EventHandler<EventArgs> OnClosed;

        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        public PickerFilterPopup(IEnumerable<object> list, string title = null)
        {
            InitializeComponent();
            SearchEntry.Unfocus();
            SetTitle(title);
            //listview.ItemSelected += Listview_ItemSelected;
            listview.ItemTapped += Listview_ItemTapped;
            if (list != null)
                SetItemSource(list);
        }

        private void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var e1 = new SelectedItemChangedEventArgs(e.Item);
            SelectedItemChanged?.Invoke(this, e1);
        }

        public void SetTitle(string text = null)
        {
            Title.Text = text;
        }

        private void Listview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedItemChanged?.Invoke(this, e);
        }

        public void SetItemSource(IEnumerable<object> list)
        {
            listview.ItemsSource = list;
        }

        public void SetItemDisplayBinding(string binding)
        {
            listview.ItemDisplayBinding = binding;
        }

        public void SetItemDisplayBinding2(string binding)
        {
            listview.ItemDisplayBinding2 = binding;
        }

        public void SetIsEnableDash(bool value)
        {
            listview.IsEnableDash = value;
        }

        public void Handle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.OldTextValue) && string.IsNullOrEmpty(e.NewTextValue))
                return;

            OnFilterTextChanged?.Invoke(this, e);
        }

        public async void ClosedClicked(object sender, EventArgs e)
        {
            OnClosed?.Invoke(this, EventArgs.Empty);
            await Navigation.PopPopupAsync();
        }

        public void VisibleSearch(bool visible)
        {
            SearchEntry.IsVisible = visible;
        }
    }
}