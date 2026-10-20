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
    /// Логика взаимодействия для ComponentsPage.xaml
    /// </summary>
    public partial class ComponentsPage : Page
    {
        private Comp _comp;

        public ComponentsPage(Comp SelectedComp)
        {
            _comp = SelectedComp;
            InitializeComponent();
            DataContext = _comp;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void AddComponent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddComponentsPage(_comp.Components));
        }
        private void RemoveComponent_Click(object sender, RoutedEventArgs e)
        {
            if (ComponentsList.SelectedItem is Component c)
            {
                _comp.Components.Remove(c);
            }
            else
            {
                MessageBox.Show("Выберите компонент для удаления");
            }
        }

        private void EditComponent_Click(object sender, MouseButtonEventArgs e)
        {
            if (ComponentsList.SelectedItem is Component c)
            {
                NavigationService.Navigate(new AddComponentsPage(_comp.Components, c));
            }
        }
    }
}
