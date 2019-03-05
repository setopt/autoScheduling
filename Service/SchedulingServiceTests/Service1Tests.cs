using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingService.Tests
{
    [TestClass()]
    public class Service1Tests
    {
        //user
        [TestMethod()]
        public void AddUserTest()
        {
            var service = new Service1();
            var userColPrev = service.SelectUser().Count;
            User user = new User
            {
                Name = "Test",
                Surname = "Test",
                Patronymic = "Test",
                Login = "Test",
                Password = "test",
                Role = "1"
            };
            User user2 = new User();
            user2 = service.AddUser(user);

            var userColPost = service.SelectUser().Count;

            service.DeleteUser(user2.ID_User);

            Assert.AreEqual(userColPrev + 1, userColPost);
        }

        [TestMethod()]
        public void AddUserTest2()
        {
            var service = new Service1();
            User userPrev = new User
            {
                Name = "dghfd",
                Surname = "dfdghf",
                Patronymic = "fdghgdfg",
                Login = "dsfsghd",
                Password = "fdsghfs",
                Role = "1",
                error = false,
                error_message = null

            };

            User userPost = service.AddUser(userPrev);
            service.DeleteUser(userPost.ID_User);

            Assert.AreEqual(userPrev, userPost);
        }

        [TestMethod()]
        public void FindByIDUserTest()
        {
            Service1 service = new Service1();
            User user = new User
            {
                Name = "asdasd",
                Surname = "tesadsadasst",
                Patronymic = "tesdfdsfdsfst",
                Login = "testsgfdgdfgt",
                Password = "tessdasdt",
                Role = "dsgfdgfdgf"
            };

            User userPrev = new User();
            userPrev = service.AddUser(user);

            User userPost = new User();
            userPost = service.FindByIDUser(userPrev.ID_User);

            service.DeleteUser(userPrev.ID_User);

            Assert.AreEqual(userPrev.ID_User, userPost.ID_User);

        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            Service1 service = new Service1();
            User user = new User
            {
                Name = "foo",
                Surname = "foo",
                Patronymic = "foo",
                Login = "foo",
                Password = "foo",
                Role = "foo"
            };

            User userPrev = new User();
            userPrev = service.AddUser(user);

            User userPlug = new User();
            userPlug = userPrev;
            userPlug.Login = "bar";
            userPlug.Name = "bar";
            userPlug.Password = "bar";
            service.UpdateUser(userPlug);


            User userPost = new User();
            userPost = service.FindByIDUser(userPrev.ID_User);

            service.DeleteUser(userPost.ID_User);

            Assert.AreNotEqual(userPrev, userPost);
        }

        //room
        [TestMethod()]
        public void AddRoomTest()
        {
            var service = new Service1();
            var roomColPrev = service.SelectRoom().Count;
            Room room = new Room
            {
                Number = "tt",
                Roominess = 16
            };
            Room room2 = new Room();
            room2 = service.AddRoom(room);

            var roomColPost = service.SelectRoom().Count;

            service.DeleteRoom(room2.ID_Room);

            Assert.AreEqual(roomColPrev + 1, roomColPost);
        }

        [TestMethod()]
        public void UpdateRoomTest()
        {
            var service = new Service1();
            Room room = new Room
            {
                Number = "tt",
                Roominess = 15
            };

            Room roomPrev = new Room();
            roomPrev = service.AddRoom(room);

            Room roomPlug = new Room();
            roomPlug = roomPrev;
            roomPlug.Number = "df";
            roomPlug.Roominess = 15;
            service.UpdateRoom(roomPlug);


            Room roomPost = new Room();
            roomPost = service.FindByIDRoom(roomPrev.ID_Room);

            service.DeleteUser(roomPost.ID_Room);

            Assert.AreNotEqual(roomPrev, roomPost);
        }

        [TestMethod()]
        public void FindByIDRoomTest()
        {
            Service1 service = new Service1();
            Room room = new Room
            {
                Number = "TestRoom",
                Roominess = 15
            };

            Room roomPrev = new Room();
            roomPrev = service.AddRoom(room);

            Room roomPost = new Room();
            roomPost = service.FindByIDRoom(roomPrev.ID_Room);

            service.DeleteRoom(roomPrev.ID_Room);

            Assert.AreEqual(roomPrev.ID_Room, roomPost.ID_Room);
        }

        //group
        [TestMethod()]
        public void AddGroupTest()
        {
            var service = new Service1();
            var groupColPrev = service.SelectGroup().Count;
            Group group = new Group
            {
                Name = "foo",
                Number = 14
            };
            Group group2 = new Group();
            group2 = service.AddGroup(group);

            var groupColPost = service.SelectGroup().Count;

            service.DeleteGroup(group2.ID_Group);

            Assert.AreEqual(groupColPrev + 1, groupColPost);
        }

        [TestMethod()]
        public void AddGroupTest2()
        {
            var service = new Service1();
            Group groupPrev = new Group
            {
                Name = "foo",
                Number = 12
            };

            Group groupPost = service.AddGroup(groupPrev);

            service.DeleteGroup(groupPost.ID_Group);

            Assert.AreEqual(groupPrev, groupPost);
        }

        [TestMethod()]
        public void FindByIDGroupTest()
        {
            Service1 service = new Service1();
            Group group = new Group
            {
                Name = "foo",
                Number = 23
            };

            Group groupPrev = new Group();
            groupPrev = service.AddGroup(group);

            Group groupPost = new Group();
            groupPost = service.FindByIDGroup(groupPrev.ID_Group);

            service.DeleteGroup(groupPrev.ID_Group);

            Assert.AreEqual(groupPrev.ID_Group, groupPost.ID_Group);
        }

        [TestMethod()]
        public void UpdateGroupTest()
        {
            Service1 service = new Service1();
            Group group = new Group
            {
                Name = "foo",
                Number = 12
            };

            Group groupPrev = new Group();
            groupPrev = service.AddGroup(group);

            Group groupPlug = new Group();
            groupPlug = groupPrev;
            groupPlug.Name = "bar";
            groupPlug.Number = 13;
            service.UpdateGroup(groupPlug);

            Group groupPost = new Group();
            groupPost = service.FindByIDGroup(groupPrev.ID_Group);

            service.DeleteGroup(groupPost.ID_Group);

            Assert.AreNotEqual(groupPrev, groupPost);
        }


        //subject
        [TestMethod()]
        public void AddSubjectTest()
        {
            var service = new Service1();
            var subjectColPrev = service.SelectSubject().Count;
            Subject subject = new Subject
            {
                Name = "foo",
            };
            Subject subject2 = new Subject();
            subject2 = service.AddSubject(subject);

            var subjectColPost = service.SelectSubject().Count;

            service.DeleteSubject(subject2.ID_Subject);

            Assert.AreEqual(subjectColPrev + 1, subjectColPost);

        }

        [TestMethod()]
        public void AddSubjectTest2()
        {
            var service = new Service1();
            Subject subjectPrev = new Subject
            {
                Name = "foo",
            };

            Subject subjectPost = new Subject();
            subjectPost = service.AddSubject(subjectPrev);

            service.DeleteSubject(subjectPost.ID_Subject);

            Assert.AreEqual(subjectPrev, subjectPost);
        }

        [TestMethod()]
        public void UpdateSubjectTest()
        {
            var service = new Service1();
            Subject subject = new Subject
            {
                Name = "foo",
            };

            Subject subjectPrev = new Subject();
            subjectPrev = service.AddSubject(subject);

            Subject subjectPlug = new Subject();
            subjectPlug = subjectPrev;
            subjectPlug.Name = "bar";
            service.UpdateSubject(subjectPlug);

            Subject subjectPost = new Subject();
            subjectPost = service.FindByIDSubject(subjectPost.ID_Subject);

            service.DeleteSubject(subjectPlug.ID_Subject);

            Assert.AreNotEqual(subjectPrev, subjectPost);

        }

        //shedule
        [TestMethod()]
        public void AddSheduleTest()
        {
            Service1 service = new Service1();
            int sheduleColPrev = service.SheduleTable().Count;
            Shedule shedule = new Shedule
            {
                Room_ID = 1,
                Order_ID = 1,
                Couple_ID = 1,
                DayOfWeek = 1,
                NumDem = true
            };

            Shedule shedulePlug = new Shedule();
            shedulePlug = service.AddShedule(shedule);

            int sheduleColPost = service.SheduleTable().Count;
            service.DeleteShedule(shedulePlug.ID_Shedule);

            Assert.AreEqual(sheduleColPrev + 1, sheduleColPost);
        }

        [TestMethod()]
        public void AddSheduleTest2()
        {
            Service1 service = new Service1();
            Shedule shedulePrev = new Shedule
            {
                Room_ID = 1,
                Order_ID = 1,
                Couple_ID = 1,
                DayOfWeek = 1,
                NumDem = true
            };

            Shedule shedulePost = new Shedule();
            shedulePost = service.AddShedule(shedulePrev);

            service.DeleteShedule(shedulePost.ID_Shedule);

            Assert.AreEqual(shedulePrev, shedulePost);
        }

        [TestMethod()]
        public void UpdateSheduleTest()
        {
            Service1 service = new Service1();
            Shedule shedule = new Shedule
            {
                Room_ID = 1,
                Order_ID = 1,
                Couple_ID = 1,
                DayOfWeek = 1,
                NumDem = true
            };

            Shedule shedulePrev = new Shedule();
            shedulePrev = service.AddShedule(shedule);

            Shedule shedulePlug = new Shedule();
            shedulePlug = shedulePrev;
            shedulePlug.NumDem = false;
            service.UpdateShedule(shedulePlug);


            Shedule shedulePost = new Shedule();
            shedulePost = service.FindByIDShedule(shedulePrev.ID_Shedule);

            service.DeleteShedule(shedulePost.ID_Shedule);

            Assert.AreNotEqual(shedulePrev, shedulePost);
        }

        [TestMethod()]
        public void FindByIDSheduleTest()
        {
            Service1 service = new Service1();
            Shedule shedule = new Shedule
            {
                Room_ID = 1,
                Order_ID = 1,
                Couple_ID = 1,
                DayOfWeek = 1,
                NumDem = true
            };

            Shedule shedulePrev = new Shedule();
            shedulePrev = service.AddShedule(shedule);

            Shedule shedulePost = new Shedule();
            shedulePost = service.FindByIDShedule(shedulePrev.ID_Shedule);

            service.DeleteShedule(shedulePost.ID_Shedule);

            Assert.AreEqual(shedulePrev.ID_Shedule, shedulePost.ID_Shedule);
        }

        // Order
        [TestMethod()]
        public void AddOrderTest()
        {
            Service1 service = new Service1();
            int orderColPrev = service.OrderTable().Count;
            Order order = new Order
            {
                User_ID = 1,
                Subject_ID = 1,
                Group_ID = 1,
                NumberLessons = 1
            };

            Order orderPlug = new Order();
            orderPlug = service.AddOrder(order);

            int orderColPost = service.OrderTable().Count;
            service.DeleteOrder(orderPlug.ID_Order);

            Assert.AreEqual(orderColPrev + 1, orderColPost);
        }

        [TestMethod()]
        public void UpdateOrderTest()
        {
            Service1 service = new Service1();
            Order order = new Order
            {
                User_ID = 1,
                Subject_ID = 1,
                Group_ID = 1,
                NumberLessons = 1
            };

            Order orderPrev = new Order();
            orderPrev = service.AddOrder(order);

            Order orderPlug = new Order();
            orderPlug = orderPrev;
            orderPlug.NumberLessons = 2;
            service.UpdateOrder(orderPlug);


            Order orderPost = new Order();
            orderPost = service.FindByIDOrder(orderPrev.ID_Order);

            service.DeleteOrder(orderPost.ID_Order);

            Assert.AreNotEqual(orderPrev, orderPost);
        }

        [TestMethod()]
        public void FindByIDOrderTest()
        {
            Service1 service = new Service1();
            Order order = new Order
            {
                User_ID = 1,
                Subject_ID = 1,
                Group_ID = 1,
                NumberLessons = 1
            };

            Order orderPrev = new Order();
            orderPrev = service.AddOrder(order);

            Order orderPost = new Order();
            orderPost = service.FindByIDOrder(orderPrev.ID_Order);

            service.DeleteOrder(orderPost.ID_Order);

            Assert.AreEqual(orderPrev.ID_Order, orderPost.ID_Order);
        }

        //couple
       /* [TestMethod()]
        public void AddCoupleTest()
        {
            var service = new Service1();
            var coupleColPrev = service.SelectCouple().Count;
            Couple couple = new Couple
            {
               // ID_Couple = 10,
                Start = TimeSpan.Parse("08:00:00"),
				End = TimeSpan.Parse("08:10:00")

            };
            Couple couple2 = new Couple();
            couple2 = service.AddCouple(couple);
			
			var coupleColPost = service.SelectCouple().Count;

            service.DeleteCouple(couple2.ID_Couple);
			
			Assert.AreEqual(coupleColPrev + 1, coupleColPost);
        }*/

       /* [TestMethod()]
        public void UpdateCoupleTest()
        {
            Service1 service = new Service1();
            Couple couple = new Couple
            {
                ID_Couple = 10,
                Start = TimeSpan.Parse("20:10"),
                End = TimeSpan.Parse("21:40")
            };

            Couple couplePrev = new Couple();
            couplePrev = service.AddCouple(couple);

            Couple couplePlug = new Couple();
            couplePlug = couplePrev;
            couplePlug.Start = TimeSpan.Parse("20:15");
			couplePlug.End = TimeSpan.Parse("21:45");
            service.UpdateCouple(couplePlug);


            Couple couplePost = new Couple();
            couplePost = service.FindByIDCouple(couplePrev.ID_Couple);

            service.DeleteCouple(couplePost.ID_Couple);

            Assert.AreNotEqual(couplePrev, couplePost);
		}*/

       /* [TestMethod()]
        public void FindByIDCoupleTest()
        {
            Service1 service = new Service1();
            Couple couple = new Couple
            {
                ID_Couple = 10,
                Start = TimeSpan.Parse("20:10"),
                End = TimeSpan.Parse("21:40")
            };

            Couple couplePrev = new Couple();
            couplePrev = service.AddCouple(couple);

            Couple couplePost = new Couple();
            couplePost = service.FindByIDCouple(couplePrev.ID_Couple);

            service.DeleteCouple(couplePrev.ID_Couple);

            Assert.AreEqual(couplePrev.ID_Couple, couplePost.ID_Couple);
        }*/

    }
}