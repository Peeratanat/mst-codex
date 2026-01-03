using IO.Swagger.Model;
using Rg.Plugins.Popup.Pages;
using System;

namespace CRMMobile.Views.Popup
{
    //public delegate void ItemHasBeenSelected(object sender, bool e);

    public partial class QualifyLeadToContact : PopupPage
    {
        public QualifyLeadToContact(LeadQualifyDTO leadQualify)
        {
            InitializeComponent();
            this.BindingContext = leadQualify;
        }

        public event EventHandler<bool> onConfirm;

        public void Accept(object sender, EventArgs args)
        {
            onConfirm?.Invoke(null, true);
        }

        public void Cancle(object obj, EventArgs e)
        {
            onConfirm?.Invoke(null, false);
        }
    }
}