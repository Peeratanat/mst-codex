using Prism.Mvvm;
using System;

namespace CRMMobile.Models
{
    public class RadioModel<T> : BindableBase
    {
        public RadioModel()
        {
            IsEnable = true;
        }

        private Guid id;

        public Guid Id
        {
            get => id;
            set { SetProperty(ref id, value); }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; RaisePropertyChanged(nameof(IsSelected)); }
        }

        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; RaisePropertyChanged(nameof(IsVisible)); }
        }

        private bool isEnable;

        public bool IsEnable
        {
            get { return isEnable; }
            set { isEnable = value; RaisePropertyChanged(nameof(IsEnable)); }
        }

        private string title;

        public string Title
        {
            get => title;
            set { SetProperty(ref title, value); RaisePropertyChanged(nameof(HasTitle)); }
        }

        public bool HasTitle { get => !string.IsNullOrEmpty(Title); }

        private string descsription;

        public string Descsription
        {
            get => descsription;
            set { SetProperty(ref descsription, value); }
        }

        private T _value;

        public T Value
        {
            get => _value;
            set { _value = value; RaisePropertyChanged(nameof(Value)); }
        }

        private RadioAttribute radioAttribute;

        public RadioAttribute RadioAttribute
        {
            get => radioAttribute;
            set
            {
                SetProperty(ref radioAttribute, value);
            }
        }
    }

    public enum RadioAttribute
    {
        None,
        Underline
    }
}