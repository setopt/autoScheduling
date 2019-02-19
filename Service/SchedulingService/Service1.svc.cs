using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SchedulingService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        readonly string connectionString = @"Data Source= ";
        //class User
        public List<User> SelectUser()
        {
            string sql= "SELECT ID_User as [ID]," +
                            "Name as [Имя]," +
                            "Surname as [Фамилия]," +
                            "Patronymic as [Отчество]," +
                            "Login as [Логин]," +
                            "Password as [Пароль]," +
                            "Role as [Роль]" +
                        "FROM [User]";
            return null;
        }

        public void AddUser(User user)
        {
            string sql = "INSERT INTO [User](Name,Surname,Patronymic,Login,Password,Role) VALUES ('w','w','w','w','w','w')";
        }

        public void UpdateUser(User user)
        {
            string sql = "UPDATE [User] SET Name = '',Surname= '',Patronymic= '',Login= '',Password= '',Role= '' WHERE [User].ID_User = 2;";
        }

        public void DeleteUser(int id)
        {
            string sql = "DELETE FROM [User] WHERE [User].ID_User = @id";
        }

        //class Group
        public List<Group> SelectGroup()
        {
            string sql = "SELECT ID_Group as [ID]," +
                            "Name as [Название группы]," +
                            "Number as [Номер группы]," +
                        "FROM [Group]";
            return null;
        }
        //class Room
        public List<Room> SelectRoom()
        {
            string sql = "SELECT ID_Room as [ID]," +
                            "Number as [Аудитория]," +
                            "Roominess as [Вместимость]," +
                        "FROM [Room]";
            return null;
        }

        public void AddRoom(Room room)
        {
            string sql = "INSERT INTO [Room](Number, Roominess) VALUES ('w','w')";
        }

        public void UpdateRoom(Room room)
        {
            string sql = "UPDATE [Room] SET Number = '',Roominess= '' WHERE [Room].ID_Room = 2;";
        }

        public void DeleteRoom(int id)
        {
            string sql = "DELETE FROM [Room] WHERE [Room].ID_Room = @id";
        }
    }

    public class User
    {
        public int ID_User;
        public string Name;
        public string Surname;
        public string Patronymic;
        public string Login;
        public string Password;
        public string Role;

        //Name,Surname,Patronymic,Login,Password,Role
    }

    public class Room
    {
        public int ID_Room;
        public string Number;
        public int Roominess;
    }

    public class Group
    {
        public int ID_Group;
        public string Name;
        public int Number;
    }
   
}
