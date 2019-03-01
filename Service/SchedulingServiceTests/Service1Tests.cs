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
                Name = "dghfgfd",
                Surname = "dfdfgghf",
                Patronymic = "fdfgghgdfg",
                Login = "dsfsgfghd",
                Password = "fdsgfghfs",
                Role = "1"
            };

            service.AddUser(user);

            var userColPost = service.SelectUser().Count;

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
            var servise = new Service1();
            var roomColPrev = servise.SelectRoom().Count;
            Room room = new Room
            {
                Number = "402",
                Roominess = 15
            };

            servise.AddRoom(room);

            var roomColPost = servise.SelectRoom().Count;

            Assert.AreEqual(roomColPrev + 1, roomColPost);
        }
        
}
}