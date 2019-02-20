using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tokin\Documents\GitHub\autoScheduling\Service\SchedulingService\App_Data\db_schedule.mdf;Integrated Security=True";
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<User> Users = new List<User>();

                    while (reader.Read())
                    {
                        User user = new User
                        {
                            ID_User = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Surname = reader.GetString(1),
                            Patronymic = reader.GetString(1),
                            Login = reader.GetString(1),
                            Password = reader.GetString(1),
                            Role = reader.GetString(1),
                        };

                        Users.Add(user);
                    }
                    return Users;
                }
                else
                {
                    return null;//Name,Surname,Patronymic,Login,Password,Role
                }

            }
        }

        public void AddUser(User user)
        {
            string sql = "INSERT INTO [User](Name,Surname,Patronymic,Login,Password,Role) VALUES (@Name,@Surname,@Patronymic,@Login,@Password,@Role)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                

                SqlParameter NameParam = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = user.Name
                };
                command.Parameters.Add(NameParam);


                SqlParameter SurnameParam = new SqlParameter
                {
                    ParameterName = "@Surname",
                    Value = user.Surname
                };
                command.Parameters.Add(SurnameParam);

                SqlParameter PatronymicParam = new SqlParameter
                {
                    ParameterName = "@Patronymic",
                    Value = user.Patronymic
                };
                command.Parameters.Add(PatronymicParam);

                SqlParameter LoginParam = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = user.Login
                };
                command.Parameters.Add(LoginParam);

                SqlParameter PasswordParam = new SqlParameter
                {
                    ParameterName = "@Password",
                    Value = user.Password
                };
                command.Parameters.Add(PasswordParam);

                SqlParameter RoleParam = new SqlParameter
                {
                    ParameterName = "@Role",
                    Value = user.Role
                };
                command.Parameters.Add(RoleParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void UpdateUser(User user)
        {
            string sql = "UPDATE [User] SET Name = @Name,Surname = @Surname,Patronymic= @Patronymic, Login= @Login, Password= @Password,Role= @Role WHERE [User].ID_User = @ID;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter IDParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = user.ID_User
                };
                command.Parameters.Add(IDParam);

                SqlParameter NameParam = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = user.Name
                };
                command.Parameters.Add(NameParam);


                SqlParameter SurnameParam = new SqlParameter
                {
                    ParameterName = "@Surname",
                    Value = user.Surname
                };
                command.Parameters.Add(SurnameParam);

                SqlParameter PatronymicParam = new SqlParameter
                {
                    ParameterName = "@Patronymic",
                    Value = user.Patronymic
                };
                command.Parameters.Add(PatronymicParam);

                SqlParameter LoginParam = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = user.Login
                };
                command.Parameters.Add(LoginParam);

                SqlParameter PasswordParam = new SqlParameter
                {
                    ParameterName = "@Password",
                    Value = user.Password
                };
                command.Parameters.Add(PasswordParam);

                SqlParameter RoleParam = new SqlParameter
                {
                    ParameterName = "@Role",
                    Value = user.Role
                };
                command.Parameters.Add(RoleParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void DeleteUser(int id)
        {
            string sql = "DELETE FROM [User] WHERE [User].ID_User = @ID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter IDParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = id
                };
                command.Parameters.Add(IDParam);

                

                var result = command.ExecuteScalar();
                connection.Close();
            }

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

        public void AddGroup(Group group)
        {
            string sql = "INSERT INTO [Group](Name, Number) VALUES ('w','w')";
        }

        public void UpdateGroup(Group group)
        {
            string sql = "UPDATE [Group] SET Name = '', Number = '' WHERE [Group].ID_Group = 2;";
        }

        public void DeleteGroup(int id)
        {
            string sql = "DELETE FROM [Group] WHERE [Group].ID_Group = @id";
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

    public class Order
    {
        public int ID_Order;
        public int User_ID;
        public int Subject_ID;
        public int Group_ID;
        public int NumberLessons;
    }
   
}
