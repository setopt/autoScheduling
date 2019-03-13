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
    /// Логика взаимодействия для Group.xaml
    /// </summary>
    public partial class Group : Window
    {
        Service.Service1Client service = new Service.Service1Client();
        private int id_group;
        public Group()
        {
            InitializeComponent();
            FillDG();
            gridDB.Visibility = Visibility.Hidden;
            dataGrid.Visibility = Visibility.Visible;
        }

        private void FillDG()
        {
            dataGrid.ItemsSource = service.SelectGroup();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (labelDB.Content.ToString() == "Добавить группу")
            {
                Service.Group group = new Service.Group
                {
                    Name = tbName.Text,
                    Number = Convert.ToInt32(tbNumber.Text)
                };
                service.AddGroup(group);
            }
            else if (labelDB.Content.ToString() == "Редактировать группу")
            {
                Service.Group group = new Service.Group
                {
                    ID_Group = id_group,
                    Name = tbName.Text,
                    Number = Convert.ToInt32(tbNumber.Text)

                };
                service.UpdateGroup(group);
            }
            btnCancel.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            FillDG();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            labelDB.Content = "Добавить группу";
            gridDB.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Hidden;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            labelDB.Content = "Редактировать группу";
            gridDB.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Hidden;

            Service.Group group = dataGrid.SelectedItem as Service.Group;
            tbName.Text = group.Name;
            tbNumber.Text = group.Number.ToString();
            id_group = group.ID_Group;
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            foreach(object SelectedItem in dataGrid.SelectedItems)
            {
                Service.Group group = SelectedItem as Service.Group;
                service.DeleteGroup(group.ID_Group);
            }
            FillDG();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            gridDB.Visibility = Visibility.Hidden;
            dataGrid.Visibility = Visibility.Visible;
            tbName.Clear();
            tbNumber.Clear();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            labelDB.Content = "Редактировать группу";
            gridDB.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Hidden;

            Service.Group group = dataGrid.SelectedItem as Service.Group;
            tbName.Text = group.Name;
            tbNumber.Text = group.Number.ToString();
            id_group = group.ID_Group;
        }

        private void GridDB_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(gridDB.IsVisible == true)
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
