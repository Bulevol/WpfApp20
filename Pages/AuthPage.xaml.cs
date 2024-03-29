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

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void TextBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            using (var db = new EmployeesEntities())
            {
                var user = db.Employees
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Login == TextBoxLogin.Text && u.Password.ToString() == PasswordBox.Password.ToString());

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    return;
                }

                MessageBox.Show("Пользователь успешно найден!");

                switch (user.Role)
                {
                    case "Администратор":
                        NavigationService?.Navigate(new AdminMenu());
                        break;
                    case "Пользователь":
                        NavigationService?.Navigate(new UserMenu());
                        break;
                    case "Менеджер":
                        NavigationService?.Navigate(new ManagerMenu());
                        break;
                    case "Покупатель":
                        NavigationService?.Navigate(new BuyerMenu());
                        break;
                    case "Страховой агент":
                        NavigationService?.Navigate(new AgentMenu());
                        break;
                    case "Водитель":
                        NavigationService?.Navigate(new DriverMenu());
                        break;
                }
            }
        }
    }
}
