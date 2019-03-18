using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
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
        //Path.GetFullPath("db_schedule.mdf")
        //readonly static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\db_schedule.mdf");
        //readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tokin\Documents\GitHub\autoScheduling\Service\SchedulingService\App_Data\db_schedule.mdf;Integrated Security=True";
        //readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\etrim\OneDrive\Документы\GitHub\autoScheduling\Service\SchedulingService\App_Data\db_schedule.mdf;Integrated Security=True";
        readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tokin\Documents\GitHub\autoScheduling\Service\SchedulingService\App_Data\db_schedule.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        //readonly string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Наталья\Source\Repos\autoScheduling5\Service\SchedulingService\App_Data\db_schedule.mdf;Integrated Security = True";


        //class User
        public List<User> SelectUser()
        {
            using (db_schedule db = new db_schedule())
            {
                List<User> users = db.User.ToList();
                return users;
            }

        }

        public User AddUser(User user)
        {
            if (user.Login != "" && user.Password != "" && user.Role != "" && user.Name != "" && user.Surname != "")
            {

                using (db_schedule db = new db_schedule())
                {
                    var count = db.User
                                .Where(s => s.Login == user.Login)
                                .Count();
                    if (count == 0)
                    {
                        db.User.Add(user);
                        db.SaveChanges();
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
            using(db_schedule db = new db_schedule())
            {
                User u = db.User
                    .Where(s => s.ID_User == user.ID_User)
                    .FirstOrDefault();
                u.Name = user.Name;
                u.Surname = user.Surname;
                u.Patronymic = user.Patronymic;
                u.Login = user.Login;
                u.Password = user.Password;
                u.Role = user.Role;

                db.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            using (db_schedule db = new db_schedule())
            {
                User user = db.User
                            .Where(o => o.ID_User == id)
                            .FirstOrDefault();
                db.User.Remove(user);
                db.SaveChanges();
            }
        }

        public User FindByIDUser(int id)
        {
            using (db_schedule db = new db_schedule())
            {
                User user = db.User
                            .Where(o => o.ID_User == id)
                            .FirstOrDefault();
                return user;
            }
        }


        //class Group
        public List<Group> SelectGroup()
        {
            using (db_schedule db = new db_schedule())
            {
                List<Group> groups = db.Group.ToList();
                return groups;
            }
        }

        public Group AddGroup(Group group)
        {
            if (group.Name != "" && group.Number != 0)
            {
                string sql = "INSERT INTO [Group](Name, Number) VALUES (@Name,@Number)";
                sql += ";SELECT SCOPE_IDENTITY();";
                string sql_check_group = "SELECT COUNT(*) FROM [Group] WHERE Name = @name";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command_check_gro = new SqlCommand(sql_check_group, connection);
                    command_check_gro.Parameters.AddWithValue("@name", group.Name);
                    int count_groups = (int)command_check_gro.ExecuteScalar();
                    if (count_groups == 0)
                    {
                        SqlCommand command = new SqlCommand(sql, connection);

                        SqlParameter NameParam = new SqlParameter
                        {
                            ParameterName = "@Name",
                            Value = group.Name
                        };
                        command.Parameters.Add(NameParam);

                        SqlParameter NumberParam = new SqlParameter
                        {
                            ParameterName = "@Number",
                            Value = group.Number
                        };
                        command.Parameters.Add(NumberParam);

                        int id = Convert.ToInt32(command.ExecuteScalar());
                        group.ID_Group = id;
                        
                        connection.Close();
                        return group;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return group;
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
            using (db_schedule db = new db_schedule())
            {
                List<Subject> subject = db.Subject.ToList();
                return subject;
            }

        }

        public Subject AddSubject(Subject subject)
        {
            using (db_schedule db = new db_schedule())
            {
                var count = db.Subject
                            .Where(s => s.Name == subject.Name)
                            .Count();
                if (count == 0)
                {
                    db.Subject.Add(subject);
                    db.SaveChanges();
                }
            }
            return subject;


        }

        public void UpdateSubject(Subject subject)
        {
            using (db_schedule db = new db_schedule())
            {
                Subject s = db.Subject
                    .Where(o => o.ID_Subject == subject.ID_Subject)
                    .FirstOrDefault();
                s.Name = subject.Name;

                db.SaveChanges();
            }
        }

        public void DeleteSubject(int id)
        {
            using (db_schedule db = new db_schedule())
            {
                Subject subject = db.Subject
                            .Where(o => o.ID_Subject == id)
                            .FirstOrDefault();
                db.Subject.Remove(subject);
                db.SaveChanges();
            }
        }

        public Subject FindByIDSubject(int id)
        {
            using (db_schedule db = new db_schedule())
            {
                Subject subject = db.Subject
                            .Where(o => o.ID_Subject == id)
                            .FirstOrDefault();
                return subject;
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

        public Room AddRoom(Room room)
        {
            if (room.Number != "" && room.Roominess != 0)
            {
                string sql = "INSERT INTO [Room](Number, Roominess) VALUES (@Number,@Roominess)";
                sql += ";SELECT SCOPE_IDENTITY();";
                string sql_check_room = "SELECT COUNT(*) FROM [Room] WHERE Number = @number";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command_check_user = new SqlCommand(sql_check_room, connection);
                    command_check_user.Parameters.AddWithValue("@number", room.Number);
                    int count_rooms = (int)command_check_user.ExecuteScalar();

                    if (count_rooms == 0)
                    {
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

                        int id = Convert.ToInt32(command.ExecuteScalar());
                        room.ID_Room = id;

                        //var result = command.ExecuteScalar();
                        connection.Close();
                        return room;
                    }
                    else
                    {
                        return room;
                    }
                }
            }
            else
            {
                return room;
            }
        }

        public void UpdateRoom(Room room)
        {
            if (room.Number != "" && room.Roominess != 0)
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

        public Couple AddCouple(Couple couple)
		{
			if(couple.Start != null && couple.End != null)
			{
				string sql = "INSERT INTO [Couple]([Start], [End]) VALUES (@Start, @End)";
				sql += ";SELECT SCOPE_IDENTITY();";
				string sql_check_start = "SELECT COUNT(*) FROM [User] WHERE TIMESTAMPDIFF(HOUR, [Start], @start) = 0 AND TIMESTAMPDIFF(MINUTE, [Start], @start) = 0";
				string sql_check_end = "SELECT COUNT(*) FROM [User] WHERE TIMESTAMPDIFF(HOUR, [End], @end) = 0 AND TIMESTAMPDIFF(MINUTE, [End], @end) = 0";
				string sql_check_start1 = "SELECT COUNT(*) FROM [User] WHERE TIMESTAMPDIFF(HOUR, [Start], @end) = 0 AND TIMESTAMPDIFF(MINUTE, [Start], @end) = 0";
				string sql_check_end1 = "SELECT COUNT(*) FROM [User] WHERE IMESTAMPDIFF(HOUR, [End], @start) = 0 AND TIMESTAMPDIFF(MINUTE, [End], @start) = 0";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					SqlCommand command_check_start = new SqlCommand(sql_check_start, connection);
					command_check_start.Parameters.AddWithValue("@start", couple.Start);
					int count_start = (int)command_check_start.ExecuteScalar();
					
					SqlCommand command_check_end = new SqlCommand(sql_check_end, connection);
					command_check_start.Parameters.AddWithValue("@end", couple.End);
					int count_end = (int)command_check_end.ExecuteScalar();
					
					SqlCommand command_check_start1 = new SqlCommand(sql_check_start1, connection);
					command_check_start.Parameters.AddWithValue("@end", couple.Start);
					int count_start1 = (int)command_check_start.ExecuteScalar();
					
					SqlCommand command_check_end1 = new SqlCommand(sql_check_end1, connection);
					command_check_start.Parameters.AddWithValue("@start", couple.End);
					int count_end1 = (int)command_check_end.ExecuteScalar();
						
					if(count_end == 0 && count_start == 0 && count_end1 == 0 && count_start1 == 0)
					{
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
						return couple;
					}
					else
					{						
						connection.Close();
						return couple;
					}					
				}
			}
			else
			{
				return couple;
			}
		}

        public void UpdateCouple(Couple couple)
        {
			if(couple.Start != null && couple.End != null)
			{
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "UPDATE [Couple] SET [Start] = @Start, [End]= @End WHERE [Couple].ID_Couple = @ID;";
				
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

        public Order AddOrder(Order order)
        {
            string sql = "INSERT INTO [Order]" +
                            "(User_ID, Subject_ID, Group_ID, NumberLessons)" +
                        "VALUES " +
                            "(@User_ID,@Subject_ID,@Group_ID,@NumberLessons)";
            sql += ";SELECT SCOPE_IDENTITY();";

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

                int id = Convert.ToInt32(command.ExecuteScalar());
                order.ID_Order = id;

                connection.Close();
                return order;
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
            using (db_schedule db = new db_schedule())
            {
                var tables = (from shedule in db.Shedule
                             join order in db.Order on shedule.Order_ID equals order.ID_Order
                             join couple in db.Couple on shedule.Couple_ID equals couple.ID_Couple
                             join room in db.Room on shedule.Couple_ID equals room.ID_Room
                             join user in db.User on order.User_ID equals user.ID_User
                             join gro in db.Group on order.Group_ID equals gro.ID_Group
                             join subject in db.Subject on order.Subject_ID equals subject.ID_Subject
                             select new
                             {
                                 shedule.ID_Shedule,
                                 User_name = user.Name,
                                 Room_number = room.Number,
                                 Subject_name = subject.Name,
                                 Group_name = gro.Name,
                                 shedule.DayOfWeek,
                                 shedule.NumDem
                             });
                List<SheduleTable> sheduleList = new List<SheduleTable>();
                foreach(var table in tables)
                {
                    SheduleTable shed = new SheduleTable
                    {
                        ID_Shedule = table.ID_Shedule,
                        User_name = table.User_name,
                        Room_number = table.Room_number,
                        Subject_name = table.Subject_name,
                        Group_name = table.Group_name,
                        DayOfWeek = table.DayOfWeek,
                        NumDem = table.NumDem
                    };
                    sheduleList.Add(shed);
                }
                return sheduleList;
            }
        }

        public Shedule AddShedule(Shedule shedule)
        {
            string sql = "INSERT INTO [Shedule]" +
                            "(Room_ID, Order_ID, Couple_ID, DayOfWeek, NumDem)" +
                        "VALUES " +
                            "(@Room_ID,@Order_ID,@Couple_ID,@DayOfWeeks,@NumDems)";
            sql += ";SELECT SCOPE_IDENTITY();";

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

                int id = Convert.ToInt32(command.ExecuteScalar());
                shedule.ID_Shedule = id;
                
                connection.Close();
                return shedule;
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
                            "NumDem=@NumDems " +
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
                        shedule.NumDem = (bool)reader["NumDem"];
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

        public bool CheckLoginUser(string login)
        {
            using (db_schedule db = new db_schedule())
            {
                User u = db.User
                           .Where(s => s.Login == login)
                           .FirstOrDefault();
                if (u != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Authentication Authentication(string login, string password)
        {
            Authentication auth = new Authentication();

            if (CheckLoginUser(login))
            {

                using (db_schedule db = new db_schedule())
                {
                    User u = db.User
                        .Where(s => s.Login == login)
                        .Where(s => s.Password == password)
                        .FirstOrDefault();

                    if (u!=null)
                    {

                        auth.error = false;
                        auth.error_message = null;
                        auth.User_ID = u.ID_User;
                        auth.Role = u.Role;
                        return auth;
                    }
                    else
                    {
                        auth.error = true;
                        auth.error_message = "Неверное имя пользователя или пароль";
                        return auth;
                    }
                }
                
            }
            else
            {
                auth.error = true;
                auth.error_message = "Неверное имя пользователя или пароль";
                return auth;
            }

        }

        //собственно алгоритм
        public void Create()
        {
            Building.PreBuild();
            Building.Build();
            for (int i = 0; i < Building.Classes.Count; i++)
                for (int j = 0; j < Building.Orders.Count; j++)
                {
                    Shedule sh = new Shedule();
                    sh.Couple_ID = Building.Classes[i].Couple;
                    sh.DayOfWeek = Building.Classes[i].Day_Of_Week;
                    sh.NumDem = Building.Classes[i].NumDel;
                    sh.Order_ID = Building.Orders[j].ID_Order;
                    sh.Room_ID = Building.Classes[i].Room;
                    AddShedule(sh);
                }
        }
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

    public class SheduleTable
    {
        public int ID_Shedule;
        public string User_name;
        public string Room_number;
        public string Subject_name;
        public string Group_name;
        public int DayOfWeek;
        public bool NumDem;

    }

    public class Authentication
    {
        public bool error;
        public string error_message;
        public int User_ID;
        public string Role;
    }

    //Всё, что нужно для выполнения алгоритма
    public class WhenClass
    {
        int couple;
        int day_of_week;
        bool numdel;
        int room;
        int roominess;

        public int Couple
        {
            get { return couple; }
            set { couple = value; }
        }

        public int Day_Of_Week
        {
            get { return day_of_week; }
            set
            {
                if (value > 0 && value < 7)
                    day_of_week = value;
            }
        }

        public bool NumDel
        {
            get { return numdel; }
            set { numdel = value; }
        }

        public int Room
        {
            get { return room; }
            set { room = value; }
        }

        public int Roominess
        {
            get { return roominess; }
            set
            {
                if (value > 0)
                    roominess = value;
            }
        }

        public WhenClass(int _c, int _d, bool _nd, int _r, int _rness)
        {
            Couple = _c;
            Day_Of_Week = _d;
            NumDel = _nd;
            Room = _r;
            Roominess = _rness;
        }
    }

    public class Matrix : ICloneable
    {
        public bool m;

        public object Clone()
        {
            return new Matrix { m = this.m };
        }
    }

    public class Building
    {
        public static Service1 ser = new Service1();
        public static List<Room> Rooms = new List<Room>();
        public static List<WhenClass> Classes = new List<WhenClass>();
        public static List<OrderTable> Orders = new List<OrderTable>();
        public static List<int> Limits = new List<int>();
        public static List<Matrix[,]> mtx = new List<Matrix[,]>();
        public static Matrix[,] Matrix;

        public static void PreBuild()
        {
            Orders = ser.OrderTable();
            Rooms = ser.SelectRoom();
            Matrix = new Matrix[Classes.Count, Orders.Count];
            fillClasses();
            //часть 1: поставить нули, где группы не помещаются в аудитории
            for (int i = 0; i < Classes.Count; i++)
                for (int j = 0; j < Orders.Count; j++)
                    if (Classes[i].Roominess < Orders[j].Group_number)
                    {
                        Matrix[i, j] = new Matrix { m = false };
                    }
        }

        public static void Build()
        {
            //часть 2: собственно выполнение алгоритма
            //Matrix[,] cmatrix = new Matrix[Classes.Count, Orders.Count];
            //for (int i = 0; i < Classes.Count; i++)
            //    for (int j = 0; j < Orders.Count; j++)
            //    {
            //        if (matrix[i, j] == null)
            //            cmatrix[i, j] = null;
            //        else
            //            cmatrix[i, j] = (Matrix)matrix[i, j].Clone();
            //    }
            //        List<int> cLimits = new List<int>();
            //for (int i = 0; i < limits.Count; i++)
            //{
            //    cLimits.Add(limits[i]);
            //}

            for (int i = 0; i < Classes.Count; i++)
                for (int j = 0; j < Orders.Count; j++)
                {
                    if (Matrix[i, j] == null)
                    {
                        for (int m = 0; m < Classes.Count; m++)
                        {
                            if (Matrix[m, j] != null)
                                if (!Matrix[m, j].m)
                                    Matrix[m, j] = new Matrix { m = false };
                        }

                        for (int m = 0; m < Orders.Count; m++)
                        {
                            if (Matrix[i, m] != null)
                                if (!Matrix[i, m].m)
                                    Matrix[i, m] = new Matrix { m = false };
                        }
                        Matrix[i, j] = new Matrix { m = true };
                        Limits[j]--;
                        if (Limits[j] != 0)
                        {
                            Matrix[i + (Classes.Count / 2), j] = new Matrix { m = true };
                            Limits[j]--;
                        }
                    }
                }
        }

        public static void fillClasses()
        {
            for (int i = 1; i < 7; i++)
                for (int j = 1; j < 9; j++)
                    for (int r = 0; r < Rooms.Count; r++)
                    {
                        Classes.Add(new WhenClass(i, j, false, Rooms[r].ID_Room, Rooms[r].Roominess));
                    }

            for (int i = 1; i < 7; i++)
                for (int j = 1; j < 9; j++)
                    for (int r = 0; r < Rooms.Count; r++)
                    {
                        Classes.Add(new WhenClass(i, j, true, Rooms[r].ID_Room, Rooms[r].Roominess));
                    }

            for (int i = 0; i < Orders.Count; i++)
            {
                Limits.Add(Orders[i].NumberLessons);
            }

        }
    }
}
