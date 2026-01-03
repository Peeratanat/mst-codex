using CRMMobile.Services;
using CRMMobile.Services.Models;
using CRMMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitorDetailPage : ContentPage
    {
        private double TranslationPoint;
        private int OldIndex = 0;
        private VisitorDetailViewModel vm;
        private IPopupMenuService service;

        public VisitorDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = this.BindingContext as VisitorDetailViewModel;
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var vm = this.BindingContext as VisitorDetailViewModel;
            if (OldIndex == 0 && OldIndex == vm.Position)
                return;

            var selectColumn = this.HeaderTab.Children[vm.Position];
            if (vm.Position > OldIndex)
            {
                var index = vm.Position - OldIndex;
                TranslationPoint += (selectColumn.Width * index);
            }
            else
            {
                var index = OldIndex - vm.Position;
                TranslationPoint -= (selectColumn.Width * index);
            }

            await UnderlineTab.TranslateTo(TranslationPoint, 0, 100);
            OldIndex = vm.Position;
        }

        private void ButtonMenu_Clicked(object sender, EventArgs e)
        {
            var menus = new List<PopupMenuItem>();
            if (vm.IsContact)
                menus.Add(new PopupMenuItem() { Id = 1, Name = "แก้ไขข้อมูล", IconName = "pencil" });
            else
                menus.Add(new PopupMenuItem() { Id = 1, Name = "เพิ่มข้อมูลลูกค้าได้", IconName = "pencil" });

            service = DependencyService.Get<IPopupMenuService>();
            service.MenuPopover(this, ToolbarItems.First(), menus);
            service.OnMenuItemSelected += Service_OnMenuItemSelected;
        }

        private async void Service_OnMenuItemSelected(object sender, PopupMenuItem e)
        {
            if (vm.IsContact)
                await vm.EditContact();
            else
                await vm.CreateContact();

            service.OnMenuItemSelected -= Service_OnMenuItemSelected;
        }
    }
}