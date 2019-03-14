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
    /// Логика взаимодействия для Subject.xaml
    /// </summary>
    public partial class Subject : Window
    {
        Service.Service1Client service = new Service.Service1Client();
        private int id_subject;
        public Subject()
        {
            InitializeComponent();
            dataGrid.ItemsSource = service.SelectSubject();
            gridDB.Visibility = Visibility.Hidden;
            dataGrid.Visibility = Visibility.Visible;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (labelDB.Content.ToString() == "Добавить предмет")
            {
                Service.Subject subject = new Service.Subject
                {
                    Name = tbName.Text
                };
                service.AddSubject(subject);
            }
            else if (labelDB.Content.ToString() == "Редактировать предмет")
            {
                Service.Subject subject = new Service.Subject
                {
                    ID_Subject = id_subject,
                    Name = tbName.Text
                };
                service.UpdateSubject(subject);
            }
            btnCancel.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            dataGrid.ItemsSource = service.SelectSubject();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            labelDB.Content = "Добавить предмет";
            gridDB.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Hidden;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                labelDB.Content = "Редактировать предмет";
                gridDB.Visibility = Visibility.Visible;
                dataGrid.Visibility = Visibility.Hidden;

                Service.Subject subject = dataGrid.SelectedItem as Service.Subject;
                tbName.Text = subject.Name;
                id_subject = subject.ID_Subject;
            }
            catch { }
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            foreach (object SelectedItem in dataGrid.SelectedItems)
            {
                Service.Group group = SelectedItem as Service.Group;
                service.DeleteGroup(group.ID_Group);
            }
            dataGrid.ItemsSource = service.SelectGroup();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            gridDB.Visibility = Visibility.Hidden;
            dataGrid.Visibility = Visibility.Visible;
            tbName.Clear();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            labelDB.Content = "Редактировать предмет";
            gridDB.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Hidden;

            Service.Subject subject = dataGrid.SelectedItem as Service.Subject;
            tbName.Text = subject.Name;
            id_subject = subject.ID_Subject;
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
