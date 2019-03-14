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
using System.Windows.Shapes;

namespace SchedulingClient
{
    /// <summary>
    /// Логика взаимодействия для MenuAdmin.xaml
    /// </summary>
    public partial class MenuAdmin : Window
    {
        public MenuAdmin()
        {
            InitializeComponent();
        }

        private void BtnGroup_Click(object sender, RoutedEventArgs e)
        {
            Group group = new Group();
            group.ShowDialog();
        }

        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.ShowDialog();
        }

        private void BtnRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = new Room();
            room.ShowDialog();
        }
    }
}
