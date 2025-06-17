using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TaskTodo.Model.DTO;
using TaskTodo.Model.Entity;
using TaskTodo.Services.intreface;

namespace XUnitTesting.controller
{
    public class UserTestCase
    {
        private readonly Mock<IUser> _service;
        public UserTestCase()
        {
            _service = new Mock<IUser>();
        }
        [Fact]
        public void Register()
        {
            var data = new AddUser {
               first_name = "brijesh",last_name="patel",email="brijesh@gmail.com",password="Brijesh@123",Role = UserRole.user
            };
            _service.Setup(x => x.RegisterUser(data)).ReturnsAsync((200, true));

            var result = _service.Object.RegisterUser(data).Result;

            Assert.True(result.Result);
            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public void Login()
        {
            var data = new LoginRequest
            {
                email = "brijesh@gmail.com",
                password = "Brijesh@123"
            };
            _service.Setup(x => x.LoginUser(data)).ReturnsAsync((200, "Admin",true));

            var result = _service.Object.LoginUser(data).Result;

            Assert.True(result.Result);
            Assert.Equal(200,result.StatusCode);
            Assert.NotNull(result.Role);
        }
    }
}
