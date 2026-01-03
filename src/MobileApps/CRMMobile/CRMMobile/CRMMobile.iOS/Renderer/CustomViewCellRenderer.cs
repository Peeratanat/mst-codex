using System;
using CRMMobile.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(ViewCell), typeof(CustomViewCellRenderer))]
namespace CRMMobile.iOS.Renderer
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;

        }
    }
}
