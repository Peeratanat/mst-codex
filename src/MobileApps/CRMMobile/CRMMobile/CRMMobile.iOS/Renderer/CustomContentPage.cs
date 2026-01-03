using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using CRMMobile.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomContentPage))]
namespace CRMMobile.iOS.Renderer
{
    public class CustomContentPage : PageRenderer
    {
        Page currentPage;
        ToolbarItem firstItem;
        public CustomContentPage()
        {
        }
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            currentPage = (this.Element as ContentPage);

            if (this.NavigationItem == null || currentPage == null)
                return;
            if (currentPage.ToolbarItems.Count > 0)
            {
                firstItem = currentPage.ToolbarItems.FirstOrDefault();
                firstItem.Clicked += FirstItem_Clicked;
            }
           
        }

        
        private void FirstItem_Clicked(object sender, EventArgs e)
        {
            if (this.NavigationController != null)
            {
                var navigationItem = this.NavigationController.TopViewController.NavigationItem;
                if (navigationItem.RightBarButtonItems.Length > 0)
                {
                    var popupView = new UIImageView(new CGRect(0, 0, 200, 200))
                    {
                        Image = UIImage.FromBundle("Logo.png"),
                        UserInteractionEnabled = true
                    };

                   var _menuController = new UIViewController
                    {
                        ModalPresentationStyle = UIModalPresentationStyle.Popover,
                        PreferredContentSize = new CGSize(200, 200),
                       
                        View = new UIView(frame:new CGRect(0,0,200, 200))
                    };

                    var item = navigationItem.RightBarButtonItems[0];
                    // var dd = this.View;
                    _menuController.PopoverPresentationController.SourceRect = new CGRect(50, 50, 300, 300);
                    //_menuController.PopoverPresentationController.SourceView = View;
                    _menuController.PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Up;
                    _menuController.PopoverPresentationController.BarButtonItem = item;
                    _menuController.PopoverPresentationController.Delegate = new PopoverDelegate();
                    PresentViewController(_menuController, true, null);

                }
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
           
        }
    }



    public class PopoverDelegate : UIPopoverPresentationControllerDelegate
    {
        public PopoverDelegate()
        {
        }

        protected override void Dispose(bool disposing)
        {
            Console.WriteLine(this.Class.Name + " disposed");
            base.Dispose(disposing);
        }

        public override UIModalPresentationStyle GetAdaptivePresentationStyle(UIPresentationController forPresentationController)
        {
            return UIModalPresentationStyle.None;
        }

        public override UIModalPresentationStyle GetAdaptivePresentationStyle(UIPresentationController controller, UITraitCollection traitCollection)
        {
            return UIModalPresentationStyle.None;
        }
    }
}
