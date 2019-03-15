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
    /// Логика взаимодействия для MenuUser.xaml
    /// </summary>
    public partial class MenuUser : Window
    {
        Service.Service1Client service = new Service.Service1Client();
        

        public MenuUser()
        {
            InitializeComponent();
            var user = service.FindByIDUser(Properties.Settings.Default.User_ID);
            lb_user.Content = user.Name + " " + user.Surname + " " + user.Patronymic;

            cbGroup.SelectedValuePath = "Value";
            cbGroup.DisplayMemberPath = "Text";

            foreach (var element in service.SelectGroup()) {
                cbGroup.Items.Add(new { Value = element.ID_Group, Text = element.Name + " " + element.Number });
            }

            cbSubject.SelectedValuePath = "Value";
            cbSubject.DisplayMemberPath = "Text";

            foreach (var element in service.SelectSubject())
            {
                cbSubject.Items.Add(new { Value = element.ID_Subject, Text = element.Name });
            }


        }

        private void TbCountLessons_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbGroup.SelectedValue != null && cbSubject.SelectedValue != null && tbCountLessons.Text != "" ) {
                Service.Order order = new Service.Order
                {
                    User_ID = Properties.Settings.Default.User_ID,
                    Subject_ID = (int)cbSubject.SelectedValue,
                    Group_ID = (int)cbGroup.SelectedValue,
                    NumberLessons = int.Parse(tbCountLessons.Text)
                };
                var addOrder = service.AddOrder(order);
                if (addOrder.ID_Order > 0) {
                    MessageBox.Show("Заявка успешно отправлена!");
                }
            }
           
        }
    }
}
