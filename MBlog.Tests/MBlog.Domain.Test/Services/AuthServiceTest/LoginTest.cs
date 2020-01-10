using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MBlog.Data.Contexts;
using MBlog.Data.Repositories;
using MBlog.Domain.Dtos;
using MBlog.Domain.Entities.MBlogEntities;
using MBlog.Domain.Helpers;
using MBlog.Domain.Interfaces.Repositories;
using MBlog.Domain.Interfaces.Services;
using MBlog.Domain.Services;
using MBlog.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace MBlog.Domain.Test.Services.AuthServiceTest
{
    public class LoginTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private IMapper _mapper;
        private IOptions<AppSettings> appSettings;
        public LoginTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = config.CreateMapper();

        }

        [Fact]
        public void Test_Login_Success()
        {
            string email = "test@test.test";
            string password = "Gg1234";
            string appId = "123456789";

            var user = DataTest.Users.FirstOrDefault(x => x.Email == email);
            Assert.IsType<User>(user);

            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(c => c.GetByEmail(email)).Returns(() => GetUserByEmailMock(email));

            IAuthService service = new AuthService(_userRepositoryMock.Object, _mapper, appSettings);

            UserDto userDto = service.Login(email, password, appId);

            Assert.IsType<UserDto>(userDto);


            Assert.Equal(user?.Email, userDto.Email);
            Assert.Equal(user?.Id, userDto.Id);

        }

        private static User GetUserByEmailMock(string email)
        {
            return DataTest.Users.FirstOrDefault(u => u.Email == email);
        }

        [Theory]
        [InlineData("test", "Gg1234", "123456789")]
        public void Test_Login_Error_Email(string email, string password, string appId)
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            //_userRepositoryMock.Setup(c => c.GetByEmail(email)).Returns(() => DataTest.Users.FirstOrDefault(u => u.Email == email));

            IAuthService service = new AuthService(_userRepositoryMock.Object, _mapper, appSettings);

            Assert.Throws<ArgumentException>(() => service.Login(email, password, appId));
        }

        [Theory]
        [InlineData("test@test.test", "", "123456789")]
        [InlineData("test@test.test", null, "123456789")]
        public void Test_Login_Error_Password(string email, string password, string appId)
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            //_userRepositoryMock.Setup(c => c.GetByEmail(email)).Returns(() => DataTest.Users.FirstOrDefault(u => u.Email == email));

            IAuthService service = new AuthService(_userRepositoryMock.Object, _mapper, appSettings);

            Assert.Throws<ArgumentException>(() => service.Login(email, password, appId));
        }

        [Theory]
        [InlineData("test2@test.test", "Gg1234", "123456789")]
        public void Test_Login_Fail_Email_Incollect(string email, string password, string appId)
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(c => c.GetByEmail(email)).Returns(() => DataTest.Users.FirstOrDefault(u => u.Email == email));

            IAuthService service = new AuthService(_userRepositoryMock.Object, _mapper, appSettings);

            UserDto userDto = service.Login(email, password, appId);

            Assert.Null(userDto);
        }

        [Theory]
        [InlineData("test@test.test", "Gg12345", "123456789")]
        public void Test_Login_Fail_Password_Incollect(string email, string password, string appId)
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(c => c.GetByEmail(email)).Returns(() => DataTest.Users.FirstOrDefault(u => u.Email == email));

            IAuthService service = new AuthService(_userRepositoryMock.Object, _mapper, appSettings);

            UserDto userDto = service.Login(email, password, appId);

            Assert.Null(userDto);
        }

    }
}
