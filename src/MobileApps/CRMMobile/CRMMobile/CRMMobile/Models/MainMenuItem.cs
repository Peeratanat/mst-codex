using Prism.Mvvm;
using Xamarin.Forms;

namespace CRMMobile.Models
{
    public class MainMenuItem : BindableBase
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string PageName { get; set; }

        private Color selectedColorItem;

        public Color SelectedColorItem
        {
            get { return selectedColorItem; }
            set { SetProperty(ref selectedColorItem, value); }
        }
    }
}