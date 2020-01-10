using MBlog.Domain.Dtos;
using MBlog.Services.Models;
using System;
using System.Threading.Tasks;

namespace MBlog.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserDto, ErrorModel>> Login(LoginCommandModel command);
        Task<Result<SuccessModel, ErrorModel>> Register(LoginCommandModel command);
        Task<Result<SuccessModel, ErrorModel>> ForgotPassword(string email);
    }
}
