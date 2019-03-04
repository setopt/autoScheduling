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
            var servise = new Service1();
            var userColPrev = servise.SelectUser().Count;
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

            User userPost = servise.AddUser(userPrev);

            Assert.AreEqual(userPrev, userPost);
        }

        [TestMethod()]
        public void AddRoomTest()
        {
            var service = new Service1();
            var roomColPrev = service.SelectRoom().Count;
            Room room = new Room
            {
                Number = "Test Room",
                Roominess = 15
            };
            Room room2 = new Room();
            room2 = service.AddRoom(room);

            var roomColPost = service.SelectRoom().Count;

            service.DeleteRoom(room2.ID_Room);

            Assert.AreEqual(roomColPrev + 1, roomColPost);
        }
        
}
}