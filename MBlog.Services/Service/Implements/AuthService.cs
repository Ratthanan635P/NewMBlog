using MBlog.CallApi.Dtos;
using MBlog.CallApi.Models;
using MBlog.CallApi.Service.Interfaces;

using System;
using System.Threading.Tasks;

namespace MBlog.CallApi.Service.Implements
{
    public class AuthService : BaseService, IAuthService
    {
        public async Task<Result<SuccessModel, ErrorModel>> ForgotPassword(string email)
        {
            //Uri url = new Uri(BaseUriUser, $"/Auth/ForgotPassword");
            Uri url = new Uri(BaseUriUser, $"/Auth/ForgotPassword?email={email}");

            Result<SuccessModel, ErrorModel> result = await GetMethodAsync<SuccessModel, ErrorModel>(url);

            return result;
        }

        public async Task<Result<UserDto, ErrorModel>> Login(LoginCommandModel command)
        {
            Uri url = new Uri(BaseUriUser, $"/Auth/Login");

            Result<UserDto, ErrorModel> result = await PostMethodAsync<UserDto, ErrorModel>(url, command);

            return result;
        }

        public async Task<Result<SuccessModel, ErrorModel>> Register(LoginCommandModel command)
        {
            Uri url = new Uri(BaseUriUser, $"/Auth/Register");

            Result<SuccessModel, ErrorModel> result = await PostMethodAsync<SuccessModel, ErrorModel>(url, command);

            return result;
        }
    }
}
