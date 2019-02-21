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
                    connection.Close();

                    return Users;

                }
                else
                {
                    connection.Close();
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

        public User FindByIDUser(int id)
        {
            string sql = "SELECT * From [User] WHERE ID_User =@ID";

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

                var reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    User user = new User();
                    while (reader.Read())
                    {                     
                        user.ID_User = reader.GetInt32(0);
                        user.Name = reader.GetString(1);
                        user.Surname = reader.GetString(1);
                        user.Patronymic = reader.GetString(1);
                        user.Login = reader.GetString(1);
                        user.Password = reader.GetString(1);
                        user.Role = reader.GetString(1);                      
                    }
                    connection.Close();

                    return user;

                }
                else
                {
                    connection.Close();
                    return null;//Name,Surname,Patronymic,Login,Password,Role
                }
            }
        }


        //class Group
        public List<Group> SelectGroup()
        {
            string sql = "SELECT ID_Group as [ID]," +
                            "Name as [Название группы]," +
                            "Number as [Номер группы]," +
                        "FROM [Group]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<Group> Groups = new List<Group>();

                    while (reader.Read())
                    {
                        Group group = new Group
                        {
                            ID_Group = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Number = reader.GetInt32(1),
                            
                        };

                        Groups.Add(group);
                    }
                    connection.Close();

                    return Groups;

                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }

        public void AddGroup(Group group)
        {
            string sql = "INSERT INTO [Group](Name, Number) VALUES (@Name,@Number)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter RoominessParam = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = group.Name
                };
                command.Parameters.Add(RoominessParam);

                SqlParameter NumberParam = new SqlParameter
                {
                    ParameterName = "@Number",
                    Value = group.Number
                };
                command.Parameters.Add(NumberParam);
                
                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void UpdateGroup(Group group)
        {
            string sql = "UPDATE [Group] SET Name = @Name,Number = @Number WHERE [Group].ID_Group = @ID;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter IDParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = group.ID_Group
                };
                command.Parameters.Add(IDParam);

                SqlParameter RoominessParam = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = group.Name
                };
                command.Parameters.Add(RoominessParam);

                SqlParameter NumberParam = new SqlParameter
                {
                    ParameterName = "@Number",
                    Value = group.Number
                };
                command.Parameters.Add(NumberParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void DeleteGroup(int id)
        {
            string sql = "DELETE FROM [Group] WHERE [Group].ID_Group = @ID";
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

        public Group FindByIDGroup(int id)
        {
            string sql = "SELECT * From [Group] WHERE ID_Group =@ID";

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

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Group group = new Group();
                    while (reader.Read())
                    {
                        group.ID_Group = reader.GetInt32(0);
                        group.Name = reader.GetString(1);
                        group.Number = reader.GetInt32(1);
                    }
                    connection.Close();

                    return group;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }


        //class Room
        public List<Room> SelectRoom()
        {
            string sql = "SELECT ID_Room as [ID]," +
                            "Number as [Аудитория]," +
                            "Roominess as [Вместимость]," +
                        "FROM [Room]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<Room> Rooms = new List<Room>();

                    while (reader.Read())
                    {
                        Room room = new Room
                        {
                            ID_Room = reader.GetInt32(0),
                            Number = reader.GetString(1),
                            Roominess = reader.GetInt32(1),
                        };

                        Rooms.Add(room);
                    }
                    connection.Close();

                    return Rooms;

                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }

        public void AddRoom(Room room)
        {
            string sql = "INSERT INTO [Room](Number, Roominess) VALUES (@Number,@Roominess)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter NumberParam = new SqlParameter
                {
                    ParameterName = "@Number",
                    Value = room.Number
                };
                command.Parameters.Add(NumberParam);

                SqlParameter RoominessParam = new SqlParameter
                {
                    ParameterName = "@Roominess",
                    Value = room.Roominess
                };
                command.Parameters.Add(RoominessParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }
    
        public void UpdateRoom(Room room)
        {
            string sql = "UPDATE [Room] SET Number = @Number,Roominess= @Roominess WHERE [Room].ID_Room = @ID;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter IDParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = room.ID_Room
                };
                command.Parameters.Add(IDParam);

                SqlParameter NumberParam = new SqlParameter
                {
                    ParameterName = "@Number",
                    Value = room.Number
                };
                command.Parameters.Add(NumberParam);

                SqlParameter RoominessParam = new SqlParameter
                {
                    ParameterName = "@Roominess",
                    Value = room.Roominess
                };
                command.Parameters.Add(RoominessParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void DeleteRoom(int id)
        {
            string sql = "DELETE FROM [Room] WHERE [Room].ID_Room = @ID";
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

        public Room FindByIDRoom(int id)
        {
            string sql = "SELECT * From [Room] WHERE ID_Room =@ID";

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

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Room room = new Room();
                    while (reader.Read())
                    {
                        room.ID_Room = reader.GetInt32(0);
                        room.Number = reader.GetString(1);
                        room.Roominess = reader.GetInt32(1);
                    }
                    connection.Close();

                    return room;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }

        //class Couple
        public List<Couple> SelectCouple()
        {
            string sql = "SELECT ID_Couple as [ID]," +
                            "Start as [Начало]," +
                            "End as [Конец]," +
                        "FROM [Couple]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<Couple> Couples = new List<Couple>();

                    while (reader.Read())
                    {
                        Couple сouple = new Couple
                        {
                            ID_Couple = reader.GetInt32(0),
                            Start = reader.GetTimeSpan(1),
                            End = reader.GetTimeSpan(1),
                        };

                        Couples.Add(сouple);
                    }
                    connection.Close();

                    return Couples;

                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }

        public void AddCouple(Couple couple)
        {
            string sql = "INSERT INTO [Couple](Start, End) VALUES (@Start,@End)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter StartParam = new SqlParameter
                {
                    ParameterName = "@Start",
                    Value = couple.Start
                };
                command.Parameters.Add(StartParam);

                SqlParameter EndParam = new SqlParameter
                {
                    ParameterName = "@End",
                    Value = couple.End
                };
                command.Parameters.Add(EndParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void UpdateCouple(Couple couple)
        {
            string sql = "UPDATE [Couple] SET Start = @Start,End= @End WHERE [Couple].ID_Couple = @ID;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter IDParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = couple.ID_Couple
                };
                command.Parameters.Add(IDParam);

                SqlParameter StartParam = new SqlParameter
                {
                    ParameterName = "@Start",
                    Value = couple.Start
                };
                command.Parameters.Add(StartParam);

                SqlParameter RoominessParam = new SqlParameter
                {
                    ParameterName = "@End",
                    Value = couple.End
                };
                command.Parameters.Add(RoominessParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void DeleteCouple(int id)
        {
            string sql = "DELETE FROM [Couple] WHERE [Couple].ID_Couple = @ID";
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

        public Couple FindByIDCouple(int id)
        {
            string sql = "SELECT * From [Couple] WHERE ID_Couple =@ID";

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

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Couple couple = new Couple();
                    while (reader.Read())
                    {
                        couple.ID_Couple = reader.GetInt32(0);
                        couple.Start = reader.GetTimeSpan(1);
                        couple.End = reader.GetTimeSpan(1);
                    }
                    connection.Close();

                    return couple;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
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

    public class Couple
    {
        public int ID_Couple;
        public TimeSpan Start;
        public TimeSpan End;
    }
}
