using CRMMobile.ViewModels;
using IO.Swagger.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactAddressForm : ContentPage
    {
        private ContactAddressFormViewModel vm;

        public ContactAddressForm()
        {
            InitializeComponent();
            Projects.OnTextFilterChanged += Projects_OnTextFilterChanged;

            Country.OnTextFilterChanged += Country_OnTextFilterChanged;
            CountryEN.OnTextFilterChanged += Country_OnTextFilterChanged;

            Provinces.OnTextFilterChanged += Provinces_OnTextFilterChanged;
            Provinces.Focused += Provinces_Focused;
            Provinces.OnSelectItemChanged += Provinces_OnSelectItemChanged;

            ProvincesEN.OnTextFilterChanged += Provinces_OnTextFilterChanged;
            ProvincesEN.Focused += ProvincesEN_Focused;
            ProvincesEN.OnSelectItemChanged += Provinces_OnSelectItemChanged;

            Districts.OnTextFilterChanged += Districts_OnTextFilterChanged;
            Districts.Focused += Districts_Focused;

            DistrictsEN.OnTextFilterChanged += Districts_OnTextFilterChanged;
            DistrictsEN.Focused += DistrictsEN_Focused;


            SubDistricts.OnTextFilterChanged += SubDistricts_OnTextFilterChanged;
            SubDistricts.Focused += SubDistricts_Focused;
            SubDistricts.OnSelectItemChanged += SubDistricts_OnSelectItemChanged;

            SubDistrictsEN.OnTextFilterChanged += SubDistricts_OnTextFilterChanged;
            SubDistrictsEN.Focused += SubDistrictsEN_Focused;
            SubDistrictsEN.OnSelectItemChanged += SubDistricts_OnSelectItemChanged;

            Provinces.CanOpen = false;
            Districts.CanOpen = false;
            SubDistricts.CanOpen = false;
            ProvincesEN.CanOpen = false;
            DistrictsEN.CanOpen = false;
            SubDistrictsEN.CanOpen = false;
        }

        private void Provinces_OnSelectItemChanged(object sender, object e)
        {
            //vm.District.Value = null;
            //vm.SubDistrict.Value = null;
            //vm.Postcode.Value = null;

            //vm.DistrictEN.Value = null;
            //vm.SubDistrictEN.Value = null;
        }

        private void SubDistricts_OnSelectItemChanged(object sender, object e)
        {
            var value = e as SubDistrictListDTO;
            vm.Postcode.Value = value.PostalCode;
        }

        private async void Projects_OnTextFilterChanged(object sender, TextChangedEventArgs e)
        {
            await vm.GetProjects(e.NewTextValue);
        }

        private async void SubDistricts_OnTextFilterChanged(object sender, TextChangedEventArgs e)
        {
            await vm.GetSubDistrict(e.NewTextValue);
        }

        private async void Districts_OnTextFilterChanged(object sender, TextChangedEventArgs e)
        {
            await vm.GetDistrict(e.NewTextValue);
        }

        private async void Provinces_OnTextFilterChanged(object sender, TextChangedEventArgs e)
        {
            await vm.GetProvince(e.NewTextValue);
        }
       
        private async void Country_OnTextFilterChanged(object sender, TextChangedEventArgs e)
        {
            await vm.GetCountries(nameTH: e.NewTextValue);
        }

        private async void SubDistricts_Focused(object sender, FocusEventArgs e)
        {
            var can = vm.CanSelectSubDistrict();
            if (!can)
            {
                await vm.DisplayAlert("แจ้งเตือน", "กรุณาเลือกอำเภอก่อน", "ปิด", null);
                return;
            }

            await vm.GetSubDistrict();
            SubDistricts.CanOpen = true;
            SubDistricts.Open();
        }

        private async void Districts_Focused(object sender, FocusEventArgs e)
        {
            var can = vm.CanSelectedDistrict();
            if (!can)
            {
                await vm.DisplayAlert("แจ้งเตือน", "กรุณาเลือกจังหวัดก่อน", "ปิด", null);
                return;
            }

            await vm.GetDistrict();
            Districts.CanOpen = can;
            Districts.Open();
        }

        private async void Provinces_Focused(object sender, FocusEventArgs e)
        {
            var can = vm.CanSelectProvince();
            if (!can)
            {
                await vm.DisplayAlert("แจ้งเตือน", "กรุณาเลือกประเทศก่อน", "ปิด", null);
                return;
            }


            Provinces.CanOpen = can;
            Provinces.Open();
        }

        private async void SubDistrictsEN_Focused(object sender, FocusEventArgs e)
        {
            var can = vm.CanSelectSubDistrict();
            if (!can)
            {
                await vm.DisplayAlert("แจ้งเตือน", "กรุณาเลือกอำเภอก่อน", "ปิด", null);
                return;
            }

            await vm.GetSubDistrict();
            SubDistrictsEN.CanOpen = true;
            SubDistrictsEN.Open();
        }

        private async void DistrictsEN_Focused(object sender, FocusEventArgs e)
        {
            var can = vm.CanSelectedDistrict();
            if (!can)
            {
                await vm.DisplayAlert("แจ้งเตือน", "กรุณาเลือกจังหวัดก่อน", "ปิด", null);
                return;
            }

            await vm.GetDistrict();
            DistrictsEN.CanOpen = can;
            DistrictsEN.Open();
        }

        private async void ProvincesEN_Focused(object sender, FocusEventArgs e)
        {
            var can = vm.CanSelectProvince();
            if (!can)
            {
                await vm.DisplayAlert("แจ้งเตือน", "กรุณาเลือกประเทศก่อน", "ปิด", null);
                return;
            }


            ProvincesEN.CanOpen = can;
            ProvincesEN.Open();
        }




        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = this.BindingContext as ContactAddressFormViewModel;
        }
    }
}