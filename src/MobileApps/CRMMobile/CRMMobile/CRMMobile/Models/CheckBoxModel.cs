using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CRMMobile.Models
{
    public class CheckBoxGroup : BindableBase
    {
        public CheckBoxGroup()
        {
            Checkboxs = new ObservableCollection<CheckBoxModel>();
        }

        public event EventHandler<CheckBoxModel> OnItemSelectedChange;

        public void Check(object obj)
        {
            var id = (Guid)obj;
            foreach (var item in Checkboxs)
            {
                item.CanRaiseEvent = false;
                if (item.Id == id)
                    item.CheckOrUnCheck(true);
                else
                    item.CheckOrUnCheck(false);

                item.CanRaiseEvent = true;
            }
        }

        public int indexDefault;

        private bool isMultiple;

        public bool IsMultiple
        {
            get => isMultiple;
            set { SetProperty(ref isMultiple, value); }
        }

        private CheckBoxModel selectedCheckboxItem;

        public CheckBoxModel SelectedCheckboxItem
        {
            get => selectedCheckboxItem;
            set { SetProperty(ref selectedCheckboxItem, value); }
        }

        private ObservableCollection<CheckBoxModel> checkboxs;

        public ObservableCollection<CheckBoxModel> Checkboxs
        {
            get => checkboxs;
            set { SetProperty(ref checkboxs, value); }
        }

        public void AddValue(object value, bool check = false)
        {
            var item = new CheckBoxModel();
            item.Value = value;
            item.IsCheck = check;
            item.OnCheckedChanged += Item_OnCheckedChanged1;
            Checkboxs.Add(item);
            RaisePropertyChanged(nameof(Checkboxs));
        }

        public void AddCheckboxValue<T>(IEnumerable<T> list, int? indexDefault = null, string SelectedProperty = null)
        {
            int index = 0;
            List<CheckBoxModel> _tmpCheck = new List<CheckBoxModel>();
            foreach (var item in list)
            {
                var _checkbox = new CheckBoxModel();
                _checkbox.Value = item;
                if (indexDefault.HasValue && indexDefault.Value == index)
                {
                    _checkbox.IsCheck = true;
                }

                if (!string.IsNullOrEmpty(SelectedProperty))
                {
                    var value = item.GetType().GetProperty(SelectedProperty).GetValue(item, null);
                    _checkbox.IsCheck = (bool)value;
                }

                _checkbox.OnCheckedChanged += Item_OnCheckedChanged1;
                _tmpCheck.Add(_checkbox);
                index++;
            }
            Checkboxs = new ObservableCollection<CheckBoxModel>(_tmpCheck);
        }

        private void Item_OnCheckedChanged1(object sender, CheckBoxModel e)
        {
            SelectedCheckboxItem = e;
            OnItemSelectedChange?.Invoke(this, e);
            if (!IsMultiple)
                Check(e.Id);
        }

        public void SetDefault(int index)
        {
            indexDefault = 0;
            Checkboxs[index].CheckOrUnCheck(true);
        }

        public void Clear()
        {
            foreach (var item in Checkboxs)
            {
                item.CanRaiseEvent = false;
                item.CheckOrUnCheck(false);
                item.CanRaiseEvent = true;
            }
        }
    }

    public class CheckBoxModel : BindableBase
    {
        public event EventHandler<CheckBoxModel> OnCheckedChanged;

        public CheckBoxModel()
        {
            Id = Guid.NewGuid();
            CanRaiseEvent = true;
            CheckCommand = new DelegateCommand(() =>
            {
                CheckOrUnCheck(!isCheck);
            });
        }

        public Guid Id { get; set; }
        public DelegateCommand CheckCommand { get; set; }
        public bool CanRaiseEvent { get; set; }

        private bool isCheck;

        public bool IsCheck
        {
            get => isCheck;
            set
            {
                SetProperty(ref isCheck, value);
                if (CanRaiseEvent) { OnCheckedChanged?.Invoke(this, this); }
            }
        }

        private object value;

        public object Value
        {
            get => value;
            set { SetProperty(ref this.value, value); }
        }

        public bool CheckOrUnCheck(bool check)
        {
            IsCheck = check;
            return IsCheck;
        }
    }
}