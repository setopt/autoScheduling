using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SchedulingClient
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Window
    {
        Service.Service1Client service = new Service.Service1Client();
        private int id_user;
        public User()
        {
            InitializeComponent();
            dataGrid.ItemsSource = service.SelectUser();
            gridDB.Visibility = Visibility.Hidden;
            dataGrid.Visibility = Visibility.Visible;

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            labelDB.Content = "Добавить пользователя";
            gridDB.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Hidden;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                labelDB.Content = "Редактировать пользователя";
                gridDB.Visibility = Visibility.Visible;
                dataGrid.Visibility = Visibility.Hidden;
            
                Service.User user = dataGrid.SelectedItem as Service.User;
                tbName.Text = user.Name;
                tbSurname.Text = user.Surname;
                tbPatronymic.Text = user.Patronymic;
                tbLogin.Text = user.Login;
                tbPassword.Text = user.Password;
                cbRole.Text = user.Role;
                id_user = user.ID_User;
            }
            catch { }
            
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            labelDB.Content = "Редактировать пользователя";
            btnUpdate.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            gridDB.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Hidden;
        }

        private void GridDB_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (gridDB.IsVisible == true)
            {
                btnAdd.IsEnabled = btnUpdate.IsEnabled = btnDel.IsEnabled = false;
            }
            else if (gridDB.IsVisible == false)
            {
                btnAdd.IsEnabled = btnUpdate.IsEnabled = btnDel.IsEnabled = true;
            }
            tbName.Clear();
            tbSurname.Clear();
            tbPassword.Clear();
            tbPatronymic.Clear();
            tbLogin.Clear();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            foreach (object SelectedItem in dataGrid.SelectedItems)
            {
                Service.User user = SelectedItem as Service.User;
                service.DeleteUser(user.ID_User);
            }
            dataGrid.ItemsSource = service.SelectUser();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (labelDB.Content.ToString() == "Добавить пользователя")
            {
                Service.User user = new Service.User()
                {
                    Name = tbName.Text,
                    Surname = tbSurname.Text,
                    Patronymic = tbPatronymic.Text,
                    Login = tbLogin.Text,
                    Password = tbPassword.Text,
                    Role = cbRole.Text
                };
                service.AddUser(user);
            }
            else if (labelDB.Content.ToString() == "Редактировать пользователя")
            {
                Service.User user = new Service.User()
                {
                    Name = tbName.Text,
                    Surname = tbSurname.Text,
                    Patronymic = tbPatronymic.Text,
                    Login = tbLogin.Text,
                    Password = tbPassword.Text,
                    Role = cbRole.Text,
                    ID_User = id_user
                };
                service.UpdateUser(user);
            }
            dataGrid.ItemsSource = service.SelectUser();
            btnCancel.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            gridDB.Visibility = Visibility.Hidden;
            dataGrid.Visibility = Visibility.Visible;
        }

        private void CbRole_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
