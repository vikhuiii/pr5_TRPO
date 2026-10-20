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
using System.Windows.Shell;

namespace pr5_moskvina
{
    /// <summary>
    /// Логика взаимодействия для AddCompPage.xaml
    /// </summary>
    public partial class AddCompPage : Page
    {
        private Comp _comp;
        private ObservableCollection<Comp> _compList;
        private Comp? _originalComp;
       public AddCompPage(ObservableCollection<Comp> compList, int nextId)
        {
            _compList = compList;
            _originalComp = null;

            _comp = new Comp { Id = nextId };
            _comp.Components = new ObservableCollection<Component>();

            InitializeComponent();
            DataContext = _comp;

        }
        public AddCompPage(ObservableCollection<Comp> compList, Comp editComp)
        {
            _compList = compList;
            _originalComp = editComp;

            _comp = new Comp
            {
                Id = editComp.Id,
                Name = editComp.Name,
                Purpose = editComp.Purpose,
                Owner = editComp.Owner,
                Components = new ObservableCollection<Component>(editComp.Components)
            };
            InitializeComponent();
            DataContext = _comp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_comp.Name))
            {
                MessageBox.Show("Введите название сборки");
                return;
            }

            if (_originalComp == null)
            {
                _compList.Add(_comp);
            }
            else
            {
                _originalComp.Id = _comp.Id;
                _originalComp.Name = _comp.Name;
                _originalComp.Purpose = _comp.Purpose;
                _originalComp.Owner = _comp.Owner;
                _originalComp.Components = new ObservableCollection<Component>(_comp.Components);
            }

            Button_Click(sender, e);
        }

        private void addComponents_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("Выберите компонент для удаления в списке");
            }
        }
    }
}
