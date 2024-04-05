using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Практика1.Models;

namespace Практика1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Page
    {
        public ObservableCollection<Employee> WorkersCollection { get; set; }
        public Workers()
        {
            InitializeComponent();
            LoadData();
            DataContext = this;
            var worker = PracticContext.GetContext().Employees.ToList();
            LViewWorker.ItemsSource = worker;
        }

        private void LoadData()
        {
            using (var context = new PracticContext())
            {
                WorkersCollection = new ObservableCollection<Employee>(context.Employees.ToList());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddWorker());
        }



        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplySorting();
        }

        private void ApplySorting()
        {
            string sortBy = (cmbSorting.SelectedItem as ComboBoxItem).Content.ToString();
            using (var context = new PracticContext())
            {
                if (sortBy == "Сортировка по имени (возрастание)")
                {
                    var sortedData = context.Employees.OrderBy(e => e.FirstName).ToList();
                    LViewWorker.ItemsSource = sortedData;
                }
                else if (sortBy == "Сортировка по имени (убывание)")
                {
                    var sortedData = context.Employees.OrderByDescending(e => e.FirstName).ToList();
                    LViewWorker.ItemsSource = sortedData;
                }
                else if (sortBy == "Сортировка по фамилии (возрастание)")
                {
                    var sortedData = context.Employees.OrderBy(e => e.SecondName).ToList();
                    LViewWorker.ItemsSource = sortedData;
                }
                else if (sortBy == "Сортировка по фамилии (убывание)")
                {
                    var sortedData = context.Employees.OrderByDescending(e => e.SecondName).ToList();
                    LViewWorker.ItemsSource = sortedData;
                }
                else if (sortBy == "Сортировка по отчеству (возрастание)")
                {
                    var sortedData = context.Employees.OrderBy(e => e.Patronymic).ToList();
                    LViewWorker.ItemsSource = sortedData;
                }
                else if (sortBy == "Сортировка по отчеству (убывание)")
                {
                    var sortedData = context.Employees.OrderByDescending(e => e.Patronymic).ToList();
                    LViewWorker.ItemsSource = sortedData;
                }
            }
        }

        private void txtSearch_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            ICollectionView view = CollectionViewSource.GetDefaultView(LViewWorker.ItemsSource);

            if (view != null)
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    view.Filter = null;
                }
                else
                {
                    view.Filter = item =>
                    {
                        Employee dataItem = item as Employee;

                        if (dataItem != null)
                        {
                            string itemName = dataItem.SecondName.ToLower();
                            string itemSurname = dataItem.FirstName.ToLower();
                            string itemPatronymic = dataItem.Patronymic.ToLower();

                            return itemName.Contains(searchText) ||
                                   itemSurname.Contains(searchText) ||
                                   itemPatronymic.Contains(searchText);
                        }

                        return false;
                    };
                }
            }
        }

        private void UpdateList_Click(object sender, RoutedEventArgs e)
        {
            Workers newWorkerPage = new Workers();
            NavigationService.Navigate(newWorkerPage);
        }

        private void LViewWorker_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LViewWorker.SelectedItem != null)
            {
                using (PracticContext db = new PracticContext())
                {
                    long id = (LViewWorker.SelectedItem as Employee).IdEmployee;
                    Employee worker = db.Employees.FirstOrDefault(w => w.IdEmployee == id);
                    EditWorker editWindow = new EditWorker(worker);
                    editWindow.DataContext = LViewWorker.SelectedItem;
                    bool? result = editWindow.ShowDialog();

                    if (result == true)
                    {
                        LoadData();
                    }
                }
            }
        }
    }
}
