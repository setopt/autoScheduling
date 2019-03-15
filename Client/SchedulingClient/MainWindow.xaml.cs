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

namespace SchedulingClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtAuthorization_Click(object sender, RoutedEventArgs e)
        {
            Service.Service1Client service = new Service.Service1Client();
            Service.Authentication auth = new Service.Authentication();

            string Login = tbLogin.Text.ToString();
            string Password = pbPassword.Password.ToString();
            auth = service.Authentication(Login, Password);
            if (!auth.error)
            {
                labelError.Visibility = Visibility.Collapsed;
                if (auth.Role == "admin")
                {
                    MenuAdmin menuAdmin = new MenuAdmin();
                    menuAdmin.Show();
                    Close();
                }
                else if (auth.Role == "user")
                {
                    MenuUser menuUser = new MenuUser();
                    menuUser.Show();
                    Close();
                }
                Properties.Settings.Default.User_ID = auth.User_ID;
                Properties.Settings.Default.Save();
                
            }
            else
            {
                labelError.Content = auth.error_message;
                labelError.Visibility = Visibility.Visible;
            }
        }
    }
}
