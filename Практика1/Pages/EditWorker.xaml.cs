using System;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Практика1.Models;

namespace Практика1.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditWorker.xaml
    /// </summary>
    public partial class EditWorker : Window
    {
        private PracticContext context;

        public EditWorker(Employee worker)
        {
            InitializeComponent();
            context = new PracticContext();
            DataContext = this;
            LoadData(worker.IdEmployee); // Вызов метода LoadData() для загрузки данных выбранного сотрудника
        }

        // Загрузка данных для указанного сотрудника
        private void LoadData(long workerId)
        {
            using (var context = new PracticContext())
            {
                Employee worker = context.Employees.Find(workerId);
                try
                {
                    if (worker != null)
                    {
                        txtId.Text = worker.IdEmployee.ToString();
                        txtWorkerName.Text = worker.FirstName;
                        txtWorkerSurname.Text = worker.SecondName;
                        txtWorkerPatronymic.Text = worker.Patronymic;
                        txtPost.Text = worker.Post;
                        txtSalary.Text = worker.Salary.ToString();
                        context.Entry(worker).State = EntityState.Modified;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        private void CleanButton_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = string.Empty;
            txtWorkerName.Text = string.Empty;
            txtWorkerSurname.Text = string.Empty;
            txtWorkerPatronymic.Text = string.Empty;
            txtPost.Text = string.Empty;
            txtSalary.Text = string.Empty;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtWorkerName.Text) ||
                string.IsNullOrWhiteSpace(txtWorkerSurname.Text))
            {
                MessageBox.Show("Заполните все поля перед сохранением.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtId.Text, out int idWorker))
            {
                MessageBox.Show("Пожалуйста, введите корректное числовое значение для ID.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Проверка, существует ли уже такой сотрудник
                Employee selectedWorker = context.Employees.FirstOrDefault(w => w.IdEmployee == idWorker);
                if (selectedWorker != null)
                {
                    // Обновление существующего сотрудника
                    selectedWorker.FirstName = txtWorkerName.Text;
                    selectedWorker.SecondName = txtWorkerSurname.Text;
                    selectedWorker.Patronymic = txtWorkerPatronymic.Text;
                    selectedWorker.Post = txtPost.Text;
                    selectedWorker.Salary = decimal.Parse(txtSalary.Text);

                    context.Entry(selectedWorker).State = EntityState.Modified; //сохранение изменений в соответсвии с виртуальной БД
                }
                else
                {
                    // Добавление нового сотрудника
                    Employee newWorker = new Employee
                    {
                        IdEmployee = idWorker,
                        FirstName = txtWorkerName.Text,
                        SecondName = txtWorkerSurname.Text,
                        Patronymic = txtWorkerPatronymic.Text,
                        Post = txtPost.Text,
                        Salary = decimal.Parse(txtSalary.Text)
                    };

                    context.Employees.Add(newWorker);
                }

                context.SaveChanges();

                MessageBox.Show("Данные сохранены успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                int workerId;

                if (int.TryParse(txtId.Text, out workerId))
                {
                    Employee selectedWorker = context.Employees.FirstOrDefault(w => w.IdEmployee == workerId);

                    if (selectedWorker != null)
                    {
                        context.Employees.Remove(selectedWorker);
                        try
                        {
                            context.Accounts.Remove(context.Accounts.FirstOrDefault(w => w.IdEmployee == workerId));
                        }
                        catch
                        {

                        }
                        Employee newWorker1 = new Employee
                        {
                            IdEmployee = workerId,
                            FirstName = "empty",
                            SecondName = "empty",
                            Patronymic = "empty"
                        };
                        context.Employees.Add(newWorker1);
                        context.SaveChanges();

                        MessageBox.Show("Карточка удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtWorkerSurname_TextChanged(object sender, TextChangedEventArgs e)
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

        private void txtSex_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBirthday_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
