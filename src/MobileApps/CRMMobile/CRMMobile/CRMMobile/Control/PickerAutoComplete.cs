using CRMMobile.Views.Popup;
using IO.Swagger.Model;
using Rg.Plugins.Popup.Extensions;
using System.Collections;
using Xamarin.Forms;

namespace CRMMobile.Control
{
    public class PickerAutoComplete : BorderEntry
    {
        public PickerAutoComplete()
        {
            Icon = Helper.FontIcons.expand;
            ImageAlignment = ImageAlignment.Right;
            IconColor = Color.FromHex("#8992A7");
            this.Focused += async (s, e) =>
            {
                Unfocus();
                var popup = new PopupAutoComplete(DropdownType, APIType);
                popup.OnSelectItemEvent += (o, e1) =>
                {
                    if (APIType == APIType.MasterCenter)
                    {
                        var tmp = e1.Item as MasterCenterDropdownDTO;
                        SetSelectedItem(tmp);
                    }
                    else
                    {
                        var tmp2 = e1.Item as ProjectDTO;
                        SetSelectedProject(tmp2);
                    }
                };
                await Navigation.PushPopupAsync(popup);
            };
        }

        private void SetSelectedProject(ProjectDTO projectDTO)
        {
            ProjectSelected = projectDTO;
            Text = ProjectSelected.ProjectNo + "-" + ProjectSelected.ProjectNameTH;
        }

        private void SetSelectedItem(MasterCenterDropdownDTO masterCenter)
        {
            SelectedItem = masterCenter;
            Text = masterCenter.Name;
        }

        public static readonly BindableProperty DropdownTypeProperty =
           BindableProperty.Create(nameof(DropdownType), typeof(string), typeof(PickerAutoComplete), default(string));

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(MasterCenterDropdownDTO), typeof(PickerAutoComplete), null, BindingMode.TwoWay,
                propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ProjectSelectedProperty =
           BindableProperty.Create(nameof(ProjectDTO), typeof(ProjectDTO), typeof(PickerAutoComplete), null);

        public MasterCenterDropdownDTO SelectedItem
        {
            get { return (MasterCenterDropdownDTO)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public string DropdownType
        {
            get { return (string)GetValue(DropdownTypeProperty); }
            set { SetValue(DropdownTypeProperty, value); }
        }

        public ProjectDTO ProjectSelected
        {
            get { return (ProjectDTO)GetValue(ProjectSelectedProperty); }
            set { SetValue(ProjectSelectedProperty, value); }
        }

        public APIType APIType
        {
            get; set;
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PickerAutoComplete)bindable).OnItemsSourceChanged((IList)oldValue, (IList)newValue);
        }

        private void OnItemsSourceChanged(IList oldValue, IList newValue)
        {
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var _selectedItem = newValue as MasterCenterDropdownDTO;
            (bindable as PickerAutoComplete).SetSelectedItem(_selectedItem);
        }
    }

    public enum APIType
    {
        MasterCenter,
        Proejct
    }
}