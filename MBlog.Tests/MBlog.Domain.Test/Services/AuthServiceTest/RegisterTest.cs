using System;
using AutoMapper;
using MBlog.Domain.Entities.MBlogEntities;
using MBlog.Domain.Helpers;
using MBlog.Domain.Interfaces.Repositories;
using MBlog.Domain.Interfaces.Services;
using MBlog.Domain.Services;
using MBlog.IoC;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace MBlog.Domain.Test.Services.AuthServiceTest
{
    public class RegisterTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private IMapper _mapper;
        private IOptions<AppSettings> appSettings;
        public RegisterTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = config.CreateMapper();
        }


        [Fact]
        public void Test_Register_Success()
        {
            string email = "test@test.test";
            string password = "Gg1234";

            _userRepositoryMock = new Mock<IUserRepository>();

            IAuthService service = new AuthService(_userRepositoryMock.Object, _mapper, appSettings);

            Assert.IsNotType<Exception>(service.Register(email, password));

        }

        [Fact]
        public void Test_Register_Email_Invalid()
        {
            string email = "tt";
            string password = "Gg1234";

            _userRepositoryMock = new Mock<IUserRepository>();


            IAuthService service = new AuthService(_userRepositoryMock.Object, _mapper, appSettings);

            service.Register(email, password);

            Assert.ThrowsAny<AggregateException>(() => service.Register(email, password).Result);
        }

        [Fact]
        public void Test_Register_Password_Invalid()
        {
            string email = "test@test.test";
            string password = "gggg";

            _userRepositoryMock = new Mock<IUserRepository>();

            IAuthService service = new AuthService(_userRepositoryMock.Object, _mapper, appSettings);

            service.Register(email, password);

            Assert.ThrowsAny<AggregateException>(() => service.Register(email, password).Result);
        }
    }
}
