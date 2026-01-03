using IO.Swagger.Model;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CRMMobile.Views.Popup
{
    public partial class OpportunityFilterPopup : PopupPage
    {
        public event EventHandler<OnOpportunityFilterEvent> OnSearchEvent;

        public event EventHandler<EventArgs> OnCloseEvent;

        public event EventHandler<TextChangedEventArgs> ProjectFilterTextChangedEventArgs;

        public List<ProjectDTO> Projects { get; set; }

        public OpportunityFilterPopup(List<ProjectDTO> projects)
        {
            InitializeComponent();
            SetProjectItemsouce(projects);
            Projectname.OnTextFilterChanged += Projectname_OnTextFilterChanged;
        }

        public void SetProjectItemsouce(List<ProjectDTO> projects)
        {
            Projectname.ItemsSource = projects;
        }

        private void Projectname_OnTextFilterChanged(object sender, TextChangedEventArgs e)
        {
            ProjectFilterTextChangedEventArgs?.Invoke(this, e);
        }

        public void OnSearchHandle(object sender, EventArgs e)
        {
            OnSearchEvent?.Invoke(this, new OnOpportunityFilterEvent()
            {
                Firstname = this.Firstname.Text,
                Lastname = this.Lastname.Text,
                Telephone = this.Telephone.Text,
                Project = (ProjectDTO)this.Projectname.SelectedItem
            });
        }

        public void ClearHandle(object sender, EventArgs e)
        {
            Firstname.Text = string.Empty;
            Lastname.Text = string.Empty;
            Telephone.Text = string.Empty;
            Projectname.Text = string.Empty;
        }

        public async void Close(object sender, EventArgs e)
        {
            await Prism.PrismApplicationBase.Current.MainPage.Navigation.PopPopupAsync();
            OnCloseEvent?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }

    public class OnOpportunityFilterEvent : EventArgs
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Telephone { get; set; }
        public ProjectDTO Project { get; set; }
    }
}