using System;
using Xamarin.Forms;

namespace CRMMobile.Layout
{
    public partial class CollapseView : ContentView
    {
        public bool IsExpand { get; set; } = true;

        public CollapseView()
        {
            InitializeComponent();
            HideOrExpand(true);
        }

        public void HeaderTapped(object sender, EventArgs e)
        {
            HideOrExpand(!IsExpand);
        }

        public void HideOrExpand(bool isExpand)
        {
            if (isExpand)
            {
                IsExpand = true;
                MyContent.IsVisible = IsExpand;
            }
            else
            {
                IsExpand = false;
                MyContent.IsVisible = IsExpand;
            }
        }
    }
}