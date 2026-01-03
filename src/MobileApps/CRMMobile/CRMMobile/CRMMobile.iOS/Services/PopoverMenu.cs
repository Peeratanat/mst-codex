using System;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
using CRMMobile.iOS.Services;
using CRMMobile.Services;
using CRMMobile.Services.Models;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(PopoverMenu))]
namespace CRMMobile.iOS.Services
{
    public class PopoverMenu : IPopupMenuService
    {
        const int rowHeight = 55;
        const int rowWidth = 200;
        UIPopoverArrowDirection PermittedArrowDirections = UIPopoverArrowDirection.Up | UIPopoverArrowDirection.Down;
        public event EventHandler<PopupMenuItem> OnMenuItemSelected;

        public void MenuPopover(Page page, ToolbarItem toolbarItem, List<PopupMenuItem> menus)
        {
            var tableItems = menus.ToArray();
            var height = menus.Count * rowHeight;
            var renderer = Platform.GetRenderer(page);
            var vc = renderer.ViewController;
            var _menuController = new UIViewController
            {
                ModalPresentationStyle = UIModalPresentationStyle.Popover,
                PreferredContentSize = new CGSize(rowWidth, height),
            };

            var table = new UITableView(_menuController.View.Bounds);
            var tableSource = new TableSource(tableItems);
            tableSource.OnRowSelected += (o, e) =>
            {
                _menuController.DismissModalViewController(true);
                OnMenuItemSelected?.Invoke(this, e);
            };
            table.Source = tableSource;

            var uiView = new UIView();
            uiView.Add(table);

            var item = vc?.ParentViewController?.NavigationItem?.RightBarButtonItems[0];
            _menuController.View = uiView;
            _menuController.PopoverPresentationController.PermittedArrowDirections = PermittedArrowDirections;
            _menuController.PopoverPresentationController.BarButtonItem = item;
            _menuController.PopoverPresentationController.Delegate = new PopoverDelegate();

            // Present
            vc.PresentViewController(_menuController, true, null);
        }
    }

    public class PopoverDelegate : UIPopoverPresentationControllerDelegate
    {

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

    public class TableSource : UITableViewSource
    {
        public delegate void RowSelectedHandeler(object sender, PopupMenuItem e);
        public event RowSelectedHandeler OnRowSelected;

        PopupMenuItem[] TableItems;
        string CellIdentifier = "TableCell";

        public TableSource(PopupMenuItem[] items)
        {
            TableItems = items;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            PopupMenuItem item = TableItems[indexPath.Row];

            if (cell == null)
            { cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier); }

            var image = UIImage.FromBundle(item.IconName);

            cell.TextLabel.Text = item.Name;
            cell.TextLabel.Font = UIFont.FromName("AP",16);
            cell.ImageView.Image = image;
            cell.ImageView.Frame = new RectangleF(0, 0, 20, 20);

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            OnRowSelected?.Invoke(this, TableItems[indexPath.Row]);
        }
    }
}
