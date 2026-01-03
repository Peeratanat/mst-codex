using CRMMobile.Models;
using IO.Swagger.Model;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactAddressPopup : PopupPage
    {
        public event EventHandler<CheckBoxModel> OnConfirmEvent;

        public ContactAddressPopup()
        {
            InitializeComponent();
        }

        public ContactAddressPopup(List<ContactAddressDTO> list)
        {
            InitializeComponent();
            this.ConfirmButton.IsEnabled = false;
            CheckBoxGroup checkBoxGroup = new CheckBoxGroup();
            checkBoxGroup.OnItemSelectedChange += CheckBoxGroup_OnItemSelectedChange;
            foreach (var item in list)
            {
                checkBoxGroup.AddValue(item);
            }
            this.BindingContext = checkBoxGroup;
        }

        private void CheckBoxGroup_OnItemSelectedChange(object sender, CheckBoxModel e)
        {
            this.ConfirmButton.IsEnabled = true;
        }

        public void OnConfirm(object sender, EventArgs e)
        {
            var checkbox = this.BindingContext as CheckBoxGroup;
            OnConfirmEvent?.Invoke(this, checkbox.SelectedCheckboxItem);
        }
    }
}