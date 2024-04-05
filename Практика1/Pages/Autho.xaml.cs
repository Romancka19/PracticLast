using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using System.Windows.Threading;
using Практика1.Models;

namespace Практика1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        public Autho()
        {
            InitializeComponent();
        }
        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            Employee noname = new Employee();
            NavigationService.Navigate(new Client(noname));
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();
            Account account = new Account();
            account = PracticContext.GetContext().Accounts.Where(p => p.Login == login && p.Password == password).FirstOrDefault();
            int accountsCount = PracticContext.GetContext().Accounts.Where(p => p.Login == login && p.Password == password).Count();
            if (accountsCount > 0)
            {
                string role = account.Role.ToString();
                MessageBox.Show("Вы вошли под " + role);
                LoadForm(role, PracticContext.GetContext().Employees.Find(long.Parse(account.IdEmployee.ToString())));
            }
            else
            {
                MessageBox.Show("Введите данные заново!");
            }
        }
        private void LoadForm(string _role, Employee employee)
        {
            switch (_role)
            {
                case "клиент":
                    NavigationService.Navigate(new Client(employee));
                    break;
                case "администратор":
                    NavigationService.Navigate(new Admin(employee));
                    break;
                default:
                    MessageBox.Show("Форма для этой роли не готова!");
                    break;
            }
        }
    }
}
