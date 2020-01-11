using MBlog.CallApi.Dtos;
using MBlog.CallApi.Models;
using System;
using System.Threading.Tasks;

namespace MBlog.CallApi.Service.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserDto, ErrorModel>> Login(LoginCommandModel command);
        Task<Result<SuccessModel, ErrorModel>> Register(LoginCommandModel command);
        Task<Result<SuccessModel, ErrorModel>> ForgotPassword(string email);
        Task<Result<SuccessModel, ErrorModel>> Update(ProfileCommands model);
        Task<Result<UserDto, ErrorModel>> GetUser(string email);
    }
}
