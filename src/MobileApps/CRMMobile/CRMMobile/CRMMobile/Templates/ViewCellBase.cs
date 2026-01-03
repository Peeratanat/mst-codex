using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class ViewCellBase : ViewCell
    {
        public ViewCellBase()
        {
        }

        public static readonly BindableProperty ParentContextProperty =
           BindableProperty.Create("ParentContext", typeof(object), typeof(ViewCellBase), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as ViewCellBase).ParentContext = newValue;
            }
        }
    }
}