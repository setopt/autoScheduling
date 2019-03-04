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
                Number = "Test Room",
                Roominess = 15
            };
            Room room2 = new Room();
            room2 = service.AddRoom(room);

            var roomColPost = service.SelectRoom().Count;

            service.DeleteRoom(room2.ID_Room);

            Assert.AreEqual(roomColPrev + 1, roomColPost);
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
                Number = 12
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

            Assert.AreEqual(groupPrev, groupPost);
        }

        [TestMethod()]
        public void FindByIDGroupTest()
        {
            Service1 service = new Service1();
            Group group = new Group
            {
                Name = "foo",
                Number = 12
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
    }
}