using System;
using System.Threading.Tasks;
using MBlog.Domain.Dtos;

namespace MBlog.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        string Register(string email, string password);
       // string RegisterUser(string email, string password);
        UserDto Login(string email, string password);  
        
        bool UpdateUser(string email, string password);
        bool ForgotPassword(string email);
        UserDto GetDataUser(string email);

    }
}
