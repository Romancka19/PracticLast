using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Page
    {
        private PracticContext context;

        public bool DialogResult { get; set; } // Add this property to the class
        public AddWorker()
        {
            InitializeComponent();
            context = new PracticContext();
        }
        private void txtWorkerSurname_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtWorkerName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtWorkerPatronymic_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPswd_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Employee newWorker = new Employee
            {
                FirstName = txtWorkerName.Text,
                SecondName = txtWorkerSurname.Text,
                Patronymic = txtWorkerPatronymic.Text,
                Post = txtPost.Text,
                
            };
            bool result = decimal.TryParse(txtSalary.Text, out decimal salary);
            if (result == true)
                newWorker.Salary = salary;


            string validationMessage = newWorker.Validate();
            if (!string.IsNullOrEmpty(validationMessage))
            {
                MessageBox.Show(validationMessage, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                context.Employees.Add(newWorker);
                context.SaveChanges();

                DialogResult = true;

                MessageBox.Show("Данные сохранены успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                //NavigationService.GoBack(); // Navigate back
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CleanButton_Click(object sender, RoutedEventArgs e)
        {
            txtWorkerName.Text = string.Empty;
            txtWorkerSurname.Text = string.Empty;
            txtWorkerPatronymic.Text = string.Empty;
            txtPost.Text = string.Empty;
            txtSalary.Text = string.Empty;
        }

        private void txtBirthday_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSex_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
