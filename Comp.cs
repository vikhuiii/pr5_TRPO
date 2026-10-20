using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pr5_moskvina
{
    public class Comp : INotifyPropertyChanged
    {
        private int _id = 0;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _purpose = "";
        public string Purpose
        {
            get => _purpose;
            set
            {
                if (_purpose != value)
                {
                    _purpose = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _owner = "";
        public string Owner
        {
            get => _owner;
            set
            {
                if (_owner != value)
                {
                    _owner = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Component> _components = new();
        public ObservableCollection<Component> Components
        {
            get => _components;
            set
            {
                if (_components != value)
                {
                    if (_components != null)
                        _components.CollectionChanged -= Components_CollectionChanged;

                    _components = value ?? new ObservableCollection<Component>();
                    _components.CollectionChanged += Components_CollectionChanged;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BuildTotal));
                }
            }
        }

        public decimal BuildTotal => Components?.Sum(c => c.Price) ?? 0m;

        public Comp()
        {
            _components.CollectionChanged += Components_CollectionChanged;
        }

        private void Components_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (Component c in e.OldItems)
                    c.PropertyChanged -= Component_PropertyChanged;

            if (e.NewItems != null)
                foreach (Component c in e.NewItems)
                    c.PropertyChanged += Component_PropertyChanged;

            OnPropertyChanged(nameof(BuildTotal));
        }

        private void Component_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Component.Price))
                OnPropertyChanged(nameof(BuildTotal));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
