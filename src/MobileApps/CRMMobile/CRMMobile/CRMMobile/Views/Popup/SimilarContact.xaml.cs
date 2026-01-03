using CRMMobile.Control;
using IO.Swagger.Model;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;

namespace CRMMobile.Views.Popup
{
    public partial class SimilarContact : PopupPage
    {
        public event EventHandler<EventArgs> OnItemSelected;

        public SimilarContact()
        {
            InitializeComponent();
        }

        public SimilarContact(List<ContactSimilarDTO> similarDTOs)
        {
            InitializeComponent();
            ContactSilimars.ItemsSource = similarDTOs;
        }

        public void Item_Tapped(object sender, EventArgs e)
        {
            var item = ((SFButton)sender).CommandParameter as ContactSimilarDTO;
            OnItemSelected?.Invoke(item, EventArgs.Empty);
        }
    }
}