using System;
using System.Threading.Tasks;
using AutoMapper;
using MBlog.Domain.Dtos;
using MBlog.Domain.Entities.MBlogEntities;
using MBlog.Domain.Helpers;
using MBlog.Domain.Interfaces.Repositories;
using MBlog.Domain.Interfaces.Services;

namespace MBlog.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto Login(string email, string password, string appId)
        {
            email = email?.Trim().ToLower();
            password = password?.Trim();

            if (!EmailHelper.IsValidEmail(email))
                throw new ArgumentException("Invalid Email.", nameof(email));

            if (!PasswordHelper.IsValidPassword(password))
                throw new ArgumentException(nameof(password));

            User user = _userRepository.GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool isValidPassword = PasswordHelper.ValidatePassword(password, user.Password);
            if (isValidPassword)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }

            return null;
        }

        public async Task<UserDto> Register(string email, string password)
        {
            email = email?.Trim().ToLower();
            password = password?.Trim();

            if (!EmailHelper.IsValidEmail(email))
                throw new ArgumentException("Invalid Email.", nameof(email));

            if (!PasswordHelper.IsValidPassword(password))
                throw new ArgumentNullException(nameof(password));

            var hashedPassword = PasswordHelper.CreatePasswordHashed(password);

            User user = new User()
            {
                Email = email,
                Password = hashedPassword
            };

            _userRepository.Add(user);
            await _userRepository.SaveChangeAsync();

            UserDto userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
