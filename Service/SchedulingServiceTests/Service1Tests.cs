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
            var servise = new Service1();
            var userColPrev = servise.SelectUser().Count;
            User user = new User
            {
                Name = "dghfd",
                Surname = "dfdghf",
                Patronymic = "fdghgdfg",
                Login = "dsfsghd",
                Password = "fdsghfs",
                Role = "1"
            };

            servise.AddUser(user);

            var userColPost = servise.SelectUser().Count;

            Assert.AreEqual(userColPrev + 1, userColPost);
        }
    }
}