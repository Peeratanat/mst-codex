using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollapseBookingLayout : ContentView
    {
        private bool _collapsed;
#pragma warning disable CS0649 // Field 'CollapseBookingLayout.OriginalHeight' is never assigned to, and will always have its default value 0
        private double OriginalHeight;
#pragma warning restore CS0649 // Field 'CollapseBookingLayout.OriginalHeight' is never assigned to, and will always have its default value 0

        public CollapseBookingLayout()
        {
            InitializeComponent();
        }

        private async void Rotate_Clicked(object sender, EventArgs e)
        {
            var rec = new Rectangle(MyContent.Bounds.X, MyContent.Bounds.Y, MyContent.Bounds.Width, OriginalHeight);
            if (_collapsed)
            {
                MyContent.IsVisible = true;
                await Task.WhenAll(new List<Task> {
                    MyContent.FadeTo(1,250),
                    MyContent.LayoutTo(rec, 500, Easing.CubicOut),
                    MyButton.RotateTo(0, 500, Easing.SpringOut),
                });
                _collapsed = false;
            }
            else
            {
                await Task.WhenAll(new List<Task> {
                    MyContent.LayoutTo(new Rectangle(MyContent.Bounds.X, MyContent.Bounds.Y, MyContent.Bounds.Width, 0), 500, Easing.CubicIn),
                    MyButton.RotateTo(180, 500, Easing.SpringOut),
                });
                MyContent.IsVisible = false;
                await MyContent.FadeTo(0, 250);
                _collapsed = true;
            }
        }
    }
}