using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr5_moskvina
{
    /// <summary>
    /// Логика взаимодействия для AddComponentsPage.xaml
    /// </summary>
    public partial class AddComponentsPage : Page
    {
        private Component _component;
        private ObservableCollection<Component> _components ;
        private Component? _originalComponent;

        public AddComponentsPage(ObservableCollection<Component> components)
        {
            _components = components;
            _originalComponent = null;
            _component = new Component();

            InitializeComponent();
            DataContext = _component;
        }
        public AddComponentsPage(ObservableCollection<Component> components, Component editComponent)
        {
            _components = components;
            _originalComponent = editComponent;

            _component = new Component
            {
                Name = editComponent.Name,
                Type = editComponent.Type,
                Price = editComponent.Price
            };

            InitializeComponent();
            DataContext = _component;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_component.Name))
            {
                MessageBox.Show("Введите название компонента");
                return;
            }

            if (_originalComponent == null)
            {
                _components.Add(_component);
            }
            else
            {
                _originalComponent.Name = _component.Name;
                _originalComponent.Type = _component.Type;
                _originalComponent.Price = _component.Price;
            }

            Button_Click(sender, e);
        }
    }
}
