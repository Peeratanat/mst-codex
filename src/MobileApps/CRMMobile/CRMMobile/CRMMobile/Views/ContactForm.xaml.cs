using CRMMobile.Layout;
using CRMMobile.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactForm : ContentPage
    {
        private ContactFormViewModel vm;
        private ControlTemplate Template1;
        private ControlTemplate Template2;

        public ContactForm()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.OnContactTypeSelected -= Vm_OnContactTypeSelected;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadTemplate();
            vm = (ContactFormViewModel)this.BindingContext;
            vm.OnContactTypeSelected += Vm_OnContactTypeSelected;
            OnSwitch();
        }

        private async void Vm_OnContactTypeSelected(object sender, bool e)
        {
            await MyContent.FadeTo(0, 100, null);
            if (e)
            {
                MyContent.ControlTemplate = Template1;
            }
            else
            {
                MyContent.ControlTemplate = Template2;
            }
            await MyContent.FadeTo(1, 100, null);
        }

        public async Task LoadTemplate()
        {
            await Task.Run(() =>
            {
                Template1 = new ControlTemplate(typeof(ContactPersonLayout));
                Template2 = new ControlTemplate(typeof(ContactCoperationLayout));
            });
        }

        private async void OnSwitch()
        {
            // await Task.Delay(300);
            Device.BeginInvokeOnMainThread(() =>
            {
                MyContent.BindingContext = vm;
                MyContent.ControlTemplate = Template1;
            });
        }
    }
}