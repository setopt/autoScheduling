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
    /// Логика взаимодействия для Room.xaml
    /// </summary>
    public partial class Room : Window
    {
        Service.Service1Client service = new Service.Service1Client();
        private int id_room;
        public Room()
        {
            InitializeComponent();
            dataGrid.ItemsSource = service.SelectRoom();
            gridDB.Visibility = Visibility.Hidden;
            dataGrid.Visibility = Visibility.Visible;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (labelDB.Content.ToString() == "Добавить аудиторию")
            {
                Service.Room room = new Service.Room
                {
                    Number = tbNumber.Text,
                    Roominess = Convert.ToInt32(tbRoominess.Text)
                };
                service.AddRoom(room);
            }
            else if (labelDB.Content.ToString() == "Редактировать аудиторию")
            {
                Service.Room room = new Service.Room
                {
                    ID_Room = id_room,
                    Number = tbNumber.Text,
                    Roominess = Convert.ToInt32(tbRoominess.Text)

                };
                service.UpdateRoom(room);
            }
            btnCancel.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            dataGrid.ItemsSource = service.SelectRoom();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            labelDB.Content = "Добавить аудиторию";
            gridDB.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Hidden;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                labelDB.Content = "Редактировать аудиторию";
                gridDB.Visibility = Visibility.Visible;
                dataGrid.Visibility = Visibility.Hidden;

                Service.Room room = dataGrid.SelectedItem as Service.Room;
                tbNumber.Text = room.Number;
                tbRoominess.Text = room.Roominess.ToString();
                id_room = room.ID_Room;
            }
            catch { }
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            foreach (object SelectedItem in dataGrid.SelectedItems)
            {
                Service.Room room = SelectedItem as Service.Room;
                service.DeleteRoom(room.ID_Room);
            }
            dataGrid.ItemsSource = service.SelectRoom();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            gridDB.Visibility = Visibility.Hidden;
            dataGrid.Visibility = Visibility.Visible;
            tbNumber.Clear();
            tbRoominess.Clear();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            labelDB.Content = "Редактировать аудиторию";
            gridDB.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Hidden;

            Service.Room room = dataGrid.SelectedItem as Service.Room;
            tbNumber.Text = room.Number;
            tbRoominess.Text = room.Roominess.ToString();
            id_room = room.ID_Room;
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
        }
    }
}
