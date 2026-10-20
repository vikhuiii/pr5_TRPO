using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        private string FilePath = "comps.json";

        public ObservableCollection<Comp> Comps { get; set; } = new();
        public Comp? SelectedComp { get; set; }
        private ObservableCollection<Component> _compons=new();
        public ListPage()
        {
            LoadFromJson();
            InitializeComponent();
            DataContext = this;
        }

        private void LoadFromJson()
        {
            if (!File.Exists(FilePath)) return;
            try
            {
                var json = File.ReadAllText(FilePath);
                var loaded = JsonSerializer.Deserialize<ObservableCollection<Comp>>(json);
                if (loaded != null) Comps = loaded;
            }
            catch { }
        }

        private void SaveToJson()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FilePath, JsonSerializer.Serialize(Comps, options));
        }

        private int GetNextId()
            => Comps.Count == 0 ? 1 : Comps.Max(c => c.Id) + 1;

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCompPage(Comps, GetNextId()));
        }

        private void EditItem_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SelectedComp == null)
            {
                MessageBox.Show("Не выбран элемент списка");
                return;
            }
            NavigationService.Navigate(new AddCompPage(Comps, SelectedComp));
        }

        private void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedComp == null)
            {
                MessageBox.Show("Выберите компьютер");
                return;
            }
            NavigationService.Navigate(new ComponentsPage(SelectedComp));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedComp == null)
            {
                MessageBox.Show("Выберите сборку для удаления");
                return;
            }

            var result = MessageBox.Show(
                $"Удалить сборку «{SelectedComp.Name}»?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Comps.Remove(SelectedComp);
                SelectedComp = null;
                SaveToJson();
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveToJson();
            MessageBox.Show("Сохранено");
        }
    }
}
