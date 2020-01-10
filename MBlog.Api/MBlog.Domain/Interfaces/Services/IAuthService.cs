using System;
using System.Threading.Tasks;
using MBlog.Domain.Dtos;

namespace MBlog.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserDto> Register(string email, string password);
        UserDto Login(string email, string password, string appId);
       
        bool UpdateUser(string email, string password);
        string ForgotPassword(string email);
        UserDto GetDataUser(string email);
    }
}
