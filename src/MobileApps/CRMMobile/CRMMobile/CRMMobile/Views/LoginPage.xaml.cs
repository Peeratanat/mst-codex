using CRMMobile.Services;
using CRMMobile.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ButtonMenu_Clicked(object sender, EventArgs e)
        {
            var menus = new List<PopupMenuItem>();
            menus.Add(new PopupMenuItem() { Id = 1, Name = "แก้ไขข้อมูล", IconName = "pencil" });

            var service = DependencyService.Get<IPopupMenuService>();
            service.MenuPopover(this, ToolbarItems.First(), menus);
            //service.OnMenuItemSelected += Service_OnMenuItemSelected;
        }
    }
}