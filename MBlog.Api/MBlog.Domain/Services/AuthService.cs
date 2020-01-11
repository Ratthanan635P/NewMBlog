using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MBlog.Domain.Dtos;
using MBlog.Domain.Entities.MBlogEntities;
using MBlog.Domain.Helpers;
using MBlog.Domain.Interfaces.Repositories;
using MBlog.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MBlog.Domain.Services
{
	public class AuthService : CenterService, IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private static Random random = new Random();
		private readonly AppSettings _appSettings;
		public AuthService(IUserRepository userRepository,
			IMapper mapper, IOptions<AppSettings> appSettings)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_appSettings = appSettings.Value;
		}
		public UserDto Login(string email, string password)
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
				userDto.AccessToken = Authenticate(user);
				return userDto;
			}

			return null;
		}
		public bool ForgotPassword(string email)
		{
			//string status = "";
			var user = _userRepository.GetByEmail(email);
			if (user != null)
			{
				string newpassword = RandomPassword();
				string newPasswordHash = PasswordHelper.CreatePasswordHashed(newpassword);
				user.Password = newPasswordHash;
				_userRepository.Update(user);
				_userRepository.SaveChange();
				SendNewPassword(email, newpassword);
				return true;
				//}
			}
			else
			{
				return false;
			}
		}

		public string RandomCode()
		{
			const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, random.Next(20, 25))
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
		public string RandomPassword()
		{
			const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890123456789";
			return new string(Enumerable.Repeat(chars, random.Next(8, 8))
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
		public bool UpdateUser(string email, string password)
		{
			//string newSalt = RandomCode();
			//string hashPassword = HashSHA256(password + newSalt);
			User result = _userRepository.GetByEmail(email);
			if (result != null)
			{
				result.Password = password;
				_userRepository.Update<User>(result);
				return true;
			}
			else
			{
				return false;
			}
		}
		public string Authenticate(User user)
		{
			// authentication successful so generate jwt token
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("JENG123456789101guhijoklsdfhgjklfsdgxfhcgj");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString()),
					new Claim(ClaimTypes.Role, user.Role.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public UserDto GetDataUser(string email)
		{
			var user = _userRepository.GetByEmail(email);
			if (user == null)
			{
				return null;
			}
			UserDto userDto = _mapper.Map<UserDto>(user);
			return userDto;
		}
		public string Register(string email, string password)
		{
			var data = _userRepository.GetByEmail(email);
			if (data != null)
			{
				return "Email is exist!";
			}
			else
			{
				//string newSalt = GenerateSalt();

				string newPassword = PasswordHelper.CreatePasswordHashed(password);
				User user = new User()
				{
					Email = email,
					Password = newPassword,
					ActiveStatus = Enums.Status.Active,
					FullName = "",
					About = "",
					AccessToken = "",
					RefeshToken = "",
					Role = Enums.Roles.User
				};
				_userRepository.Add(user);
				_userRepository.SaveChange();
				return "Success";
			}
		}

		public bool UpdateProfile(ProfileCommand profile)
		{
			var user = _userRepository.GetByEmail(profile.Email);
			if (user != null)
			{
				user.FullName = profile.FullName;
				user.About = profile.About;
				user.ImageProfile = profile.ImageProfile;
				user.ImageProfilePath = profile.ImageProfilePath;
				_userRepository.Update(user);
				_userRepository.SaveChange();
				return true;
			}
			else
			{
				return false;
			}
		}		
	}
}
