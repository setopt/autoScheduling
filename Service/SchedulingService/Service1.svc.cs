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
        readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tokin\source\repos\autoScheduling2\Service\SchedulingService\App_Data\db_schedule.mdf;Integrated Security=True";

        //class User
        public List<User> SelectUser()
        {
            string sql = "SELECT ID_User as [ID]," +
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

        public User AddUser(User user)
        {
            if (user.Login != "" && user.Password != "" && user.Role != "" && user.Name != "" && user.Surname != "")
            {
                string sql = "INSERT INTO [User](Name,Surname,Patronymic,Login,Password,Role) VALUES (@Name,@Surname,@Patronymic,@Login,@Password,@Role)";
                string sql_check_user = "SELECT COUNT(*) FROM [User] WHERE login = @login";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command_check_user = new SqlCommand(sql_check_user, connection);
                    command_check_user.Parameters.AddWithValue("@login", user.Login);
                    int count_user = (int)command_check_user.ExecuteScalar();



                    if (count_user == 0)
                    {
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
                        user.error = false;

                        return user;
                    }
                    else
                    {
                        user.error = true;
                        user.error_message = "Пользователь с таким логином уже существует!";
                        return user;
                    }



                }
            }
            else {
                user.error = true;
                user.error_message = "Заполнены не все данные!";
                return user;
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
                            "Number as [Количество учеников]" +
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
                            Number = reader.GetInt32(2),

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
                        group.Number = reader.GetInt32(2);
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

        //class Subject
        public List<Subject> SelectSubject()
        {
            string sql = "SELECT ID_Subject as [ID]," +
                            "Name as [Предмет]" +
                        "FROM [Subject]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<Subject> Subjects = new List<Subject>();

                    while (reader.Read())
                    {
                        Subject subject = new Subject
                        {
                            ID_Subject = reader.GetInt32(0),
                            Name = reader.GetString(1),
                        };

                        Subjects.Add(subject);
                    }
                    connection.Close();

                    return Subjects;

                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }

        public void AddSubject(Subject subject)
        {
            string sql = "INSERT INTO [Subject](Name) VALUES (@Name)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter NameParam = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = subject.Name
                };
                command.Parameters.Add(NameParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void UpdateSubject(Subject subject)
        {
            string sql = "UPDATE [Subject] SET Name = @Name WHERE [Subject].ID_Subject = @ID;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter IDParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = subject.ID_Subject
                };
                command.Parameters.Add(IDParam);

                SqlParameter NameParam = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = subject.Name
                };
                command.Parameters.Add(NameParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void DeleteSubject(int id)
        {
            string sql = "DELETE FROM [Subject] WHERE [Subject].ID_Subject = @ID";
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

        public Subject FindByIDSubject(int id)
        {
            string sql = "SELECT * From [Subject] WHERE ID_Subject =@ID";

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
                    Subject subject = new Subject();
                    while (reader.Read())
                    {
                        subject.ID_Subject = reader.GetInt32(0);
                        subject.Name = reader.GetString(1);
                    }
                    connection.Close();

                    return subject;
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
                            "Roominess as [Вместимость]" +
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
                            Roominess = reader.GetInt32(2),
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
                        room.Roominess = reader.GetInt32(2);
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
                            "[Start] as [Начало]," +
                            "[End] as [Конец]" +
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
                            End = reader.GetTimeSpan(2),
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
            string sql = "INSERT INTO [Couple]([Start], [End]) VALUES (@Start,@End)";
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
            string sql = "UPDATE [Couple] SET [Start] = @Start, [End]= @End WHERE [Couple].ID_Couple = @ID;";
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
                        couple.End = reader.GetTimeSpan(2);
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

        //class Order
        public List<OrderTable> OrderTable()
        {
            string sql = "SELECT" +
                        "[Order].Id_Order as [ID]," +
                        "[User].Login as [Логин]," +
                        "[User].Name as [Имя]," +
                        "[Subject].Name as [Предмет]," +
                        "[Group].Name as [Группа]," +
                        "[Group].Number as [Количество учеников]," +
                        "[Order].NumberLessons as [Количество занятий]" +
                    "FROM" +
                        "[Order],[Subject],[Group],[User]" +
                    "WHERE " +
                        "[Order].User_ID = [User].ID_User AND" +
                        "[Order].Group_ID =[Group].ID_Group AND" +
                        "[Order].Subject_ID = [Subject].ID_Subject";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<OrderTable> orderTables = new List<OrderTable>();

                    while (reader.Read())
                    {
                        OrderTable orderTable = new OrderTable
                        {
                            ID_Order = reader.GetInt32(0),
                            User_login = reader.GetString(1),
                            User_name = reader.GetString(2),
                            Subject_name = reader.GetString(3),
                            Group_name = reader.GetString(4),
                            Group_number = reader.GetInt32(5),
                            NumberLessons = reader.GetInt32(6),
                        };

                        orderTables.Add(orderTable);
                    }
                    connection.Close();

                    return orderTables;

                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }

        public void DeleteOrder(int id)
        {
            string sql = "DELETE FROM " +
                            "[Order] " +
                        "WHERE " +
                            "[Order].ID_Order = @ID";
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

        public void AddOrder(Order order)
        {
            string sql = "INSERT INTO [Order]" +
                            "(User_ID, Subject_ID, Group_ID,NumberLessons) " +
                        "VALUES " +
                            "(@User_ID,@Subject_ID,@Group_ID,@NumberLessons)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);


                SqlParameter User_IDParam = new SqlParameter
                {
                    ParameterName = "@User_ID",
                    Value = order.User_ID
                };
                command.Parameters.Add(User_IDParam);

                SqlParameter Subject_IDParam = new SqlParameter
                {
                    ParameterName = "@Subject_ID",
                    Value = order.Subject_ID
                };
                command.Parameters.Add(Subject_IDParam);

                SqlParameter Group_IDParam = new SqlParameter
                {
                    ParameterName = "@Group_ID",
                    Value = order.Group_ID
                };
                command.Parameters.Add(Group_IDParam);

                SqlParameter NumberLessonsParam = new SqlParameter
                {
                    ParameterName = "@NumberLessons",
                    Value = order.NumberLessons
                };
                command.Parameters.Add(NumberLessonsParam);



                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void UpdateOrder(Order order)
        {
            string sql = "UPDATE " +
                            "[Order] " +
                        "SET " +
                            "User_ID=@User_ID," +
                            "Subject_ID=@Subject_ID," +
                            "Group_ID=@Group_ID," +
                            "NumberLessons=@NumberLessons " +
                        "WHERE " +
                            "[Order].ID_Order = @ID";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter IDParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = order.ID_Order
                };
                command.Parameters.Add(IDParam);

                SqlParameter User_IDParam = new SqlParameter
                {
                    ParameterName = "@User_ID",
                    Value = order.User_ID
                };
                command.Parameters.Add(User_IDParam);

                SqlParameter Subject_IDParam = new SqlParameter
                {
                    ParameterName = "@Subject_ID",
                    Value = order.Subject_ID
                };
                command.Parameters.Add(Subject_IDParam);

                SqlParameter Group_IDParam = new SqlParameter
                {
                    ParameterName = "@Group_ID",
                    Value = order.Group_ID
                };
                command.Parameters.Add(Group_IDParam);

                SqlParameter NumberLessonsParam = new SqlParameter
                {
                    ParameterName = "@NumberLessons",
                    Value = order.NumberLessons
                };
                command.Parameters.Add(NumberLessonsParam);



                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public Order FindByIDOrder(int id)
        {
            string sql = "SELECT * FROM [Order] WHERE [Order].ID_Order = @ID";
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
                    Order order = new Order();

                    while (reader.Read())
                    {
                        order.ID_Order = (int)reader["ID_Order"];
                        order.User_ID = (int)reader["User_ID"];
                        order.Subject_ID = (int)reader["Subject_ID"];
                        order.Group_ID = (int)reader["Group_ID"];
                        order.NumberLessons = (int)reader["NumberLessons"];
                    }
                    connection.Close();

                    return order;

                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }

        //class Shedule
        public List<SheduleTable> SheduleTable()
        {
            string sql = "SELECT" +
                        "[Shedule].ID_Shedule as [ID]," +
                        "[User].Name as [Имя]," +
                        "[Room].Number as [Аудитория]," +
                        "[Subject].Name as [Предмет]," +
                        "[Group].Name as [Группа]," +
                        "[Shedule].DayOfWeek as [День недели]," +
                        "[Shedule].NumDem as [Числитель/Знаменатель]" +
                    "FROM" +
                        "[Shedule],[Subject],[Group],[Room],[User]" +
                    "WHERE " +
                        "[Shedule].Shedule_ID = [Shedule].ID_Shedule AND" +
                        "[Shedule].Group_ID = [Group].ID_Group AND" +
                        "[Shedule].Subject_ID = [Subject].ID_Subject AND" +
                        "[Shedule].Room_ID = [Room].ID_Room AND" +
                        "[Shedule].User_ID = [User].ID_User";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<SheduleTable> sheduleTables = new List<SheduleTable>();

                    while (reader.Read())
                    {
                        SheduleTable sheduleTable = new SheduleTable
                        {
                            ID_Shedule = reader.GetInt32(0),
                            User_name = reader.GetString(1),
                            Subject_name = reader.GetString(2),
                            Group_name = reader.GetString(3),
                            DayOfWeek = reader.GetInt32(4),
                            Room_number = reader.GetString(5),
                            NumDem = reader.GetString(6),
                        };

                        sheduleTables.Add(sheduleTable);
                    }
                    connection.Close();

                    return sheduleTables;

                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }

        public void AddShedule(Shedule shedule)
        {
            string sql = "INSERT INTO [Shedule]" +
                            "(Room_ID, Order_ID, Couple_ID, DayOfWeek, NumDem)" +
                        "VALUES " +
                            "(@Room_ID,@Order_ID,@Couple_ID,@DayOfWeeks,@NumDems)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);


                SqlParameter Room_IDParam = new SqlParameter
                {
                    ParameterName = "@Room_ID",
                    Value = shedule.Room_ID
                };
                command.Parameters.Add(Room_IDParam);

                SqlParameter Order_IDParam = new SqlParameter
                {
                    ParameterName = "@Order_ID",
                    Value = shedule.Order_ID
                };
                command.Parameters.Add(Order_IDParam);

                SqlParameter Couple_IDParam = new SqlParameter
                {
                    ParameterName = "@Couple_ID",
                    Value = shedule.Couple_ID
                };
                command.Parameters.Add(Couple_IDParam);

                SqlParameter DayOfWeeksParam = new SqlParameter
                {
                    ParameterName = "@DayOfWeeks",
                    Value = shedule.DayOfWeek
                };
                command.Parameters.Add(DayOfWeeksParam);

                SqlParameter NumDemsParam = new SqlParameter
                {
                    ParameterName = "@NumDems",
                    Value = shedule.NumDem
                };
                command.Parameters.Add(NumDemsParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void UpdateShedule(Shedule shedule)
        {
            string sql = "UPDATE " +
                            "[Shedule] " +
                        "SET " +
                            "Room_ID=@Room_ID," +
                            "Order_ID=@Order_ID," +
                            "Couple_ID=@Couple_ID," +
                            "DayOfWeek=@DayOfWeeks," +
                            "NumDem=@NumDems" +
                        "WHERE " +
                            "[Shedule].ID_Shedule = @ID";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlParameter IDParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = shedule.ID_Shedule
                };
                command.Parameters.Add(IDParam);

                SqlParameter Room_IDParam = new SqlParameter
                {
                    ParameterName = "@Room_ID",
                    Value = shedule.Room_ID
                };
                command.Parameters.Add(Room_IDParam);

                SqlParameter Order_IDParam = new SqlParameter
                {
                    ParameterName = "@Order_ID",
                    Value = shedule.Order_ID
                };
                command.Parameters.Add(Order_IDParam);

                SqlParameter Couple_IDParam = new SqlParameter
                {
                    ParameterName = "@Couple_ID",
                    Value = shedule.Couple_ID
                };
                command.Parameters.Add(Couple_IDParam);

                SqlParameter DayOfWeeksParam = new SqlParameter
                {
                    ParameterName = "@DayOfWeeks",
                    Value = shedule.DayOfWeek
                };
                command.Parameters.Add(DayOfWeeksParam);

                SqlParameter NumDemsParam = new SqlParameter
                {
                    ParameterName = "@NumDems",
                    Value = shedule.NumDem
                };
                command.Parameters.Add(NumDemsParam);

                var result = command.ExecuteScalar();
                connection.Close();
            }
        }

        public void DeleteShedule(int id)
        {
            string sql = "DELETE FROM " +
                            "[Shedule] " +
                        "WHERE " +
                            "[Shedule].ID_Shedule = @ID";
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

        public Shedule FindByIDShedule(int id)
        {
            string sql = "SELECT * FROM [Shedule] WHERE [Shedule].ID_Shedule = @ID";
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
                    Shedule shedule = new Shedule();

                    while (reader.Read())
                    {
                        shedule.ID_Shedule = (int)reader["ID_Shedule"];
                        shedule.Room_ID = (int)reader["Room_ID"];
                        shedule.Order_ID = (int)reader["Order_ID"];
                        shedule.Couple_ID = (int)reader["Couple_ID"];
                        shedule.DayOfWeek = (int)reader["DayOfWeek"];
                        shedule.NumDem = (string)reader["NumDem"];
                    }
                    connection.Close();

                    return shedule;

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
        public bool error;
        public string error_message;
    }

    public class Room
    {
        public int ID_Room;
        public string Number;
        public int Roominess;
    }

    public class Subject
    {
        public int ID_Subject;
        public string Name;
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

    public class OrderTable
    {
        public int ID_Order;
        public string User_login;
        public string User_name;
        public string Subject_name;
        public string Group_name;
        public int Group_number;
        public int NumberLessons;
    }

    public class Shedule
    {
        public int ID_Shedule;
        public int Room_ID;
        public int Order_ID;
        public int Couple_ID;
        public int DayOfWeek;
        public string NumDem;
    }

    public class SheduleTable
    {
        public int ID_Shedule;
        public string User_name;
        public string Room_number;
        public string Subject_name;
        public string Group_name;
        public int DayOfWeek;
        public string NumDem;

    }

    public class Couple
    {
        public int ID_Couple;
        public TimeSpan Start;
        public TimeSpan End;
    }
}
