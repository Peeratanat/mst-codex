using Android.Graphics;
using Android.Widget;
using CRMMobile.Droid;
using CRMMobile.Droid.Effects;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(MenuPopover), "MenuPopoverEffect")]
namespace CRMMobile.Droid.Effects
{
    public class MenuPopover : PlatformEffect
    {
		Android.Widget.PopupMenu ToggleMenu;
		InternalPopupEffect Effect;

		public MenuPopover()
        {
        }

        protected override void OnAttached()
        {
			Effect = (InternalPopupEffect)Element.Effects.FirstOrDefault(e => e is InternalPopupEffect);

			if (Effect != null)
				Effect.Parent.OnPopupRequest += OnPopupRequest;

			if (Control != null)
			{
				ToggleMenu = new Android.Widget.PopupMenu(Forms.Context, Control);
				ToggleMenu.MenuItemClick += MenuItemClick;
			}

			else if (Container != null)
			{
				ToggleMenu = new Android.Widget.PopupMenu(Forms.Context, Container);
				ToggleMenu.MenuItemClick += MenuItemClick;
			}
		}

        protected override void OnDetached()
        {
            throw new System.NotImplementedException();
        }
    }
}
