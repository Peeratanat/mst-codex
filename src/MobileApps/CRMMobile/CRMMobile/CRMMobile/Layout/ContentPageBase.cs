using Xamarin.Forms;

namespace CRMMobile.Layout
{
    public class ContentPageBase : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //var title = new Label()
            //{
            //    Text = "Contact",
            //    FontSize = 20,
            //    TextColor = Color.White,
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalTextAlignment = TextAlignment.Center,
            //    HorizontalTextAlignment = TextAlignment.Center,
            //    //FontFamily =
            //};

            //var grid = new Grid();
            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) });
            //grid.Children.Add(title, 0, 0);

            //NavigationPage.SetTitleView(this, title);
            NavigationPage.SetBackButtonTitle(this, " ");
        }
    }
}