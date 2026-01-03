using CRMMobile.Services.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CRMMobile.Services
{
    public interface IPopupMenuService
    {
        void MenuPopover(Page page, ToolbarItem toolbarItem, List<PopupMenuItem> menus);

        event EventHandler<PopupMenuItem> OnMenuItemSelected;
    }
}