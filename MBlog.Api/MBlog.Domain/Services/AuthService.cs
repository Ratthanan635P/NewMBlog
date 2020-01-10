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
    public class AuthService : IAuthService
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

            //bool isValidPassword = PasswordHelper.ValidatePassword(password, user.Password);
            //if (isValidPassword)
            //{
				UserDto userDto = new UserDto();//_mapper.Map<UserDto>(user);
				userDto.Email = user.Email;
				userDto.Id = user.Id;
				userDto.AccessToken = user.AccessToken;
			    userDto.ErrorMessage="PASS";

				return userDto;
           // }

           // return null;
        }

   //     public UserDto Register(string email, string password)
   //     {
   //         email = email?.Trim().ToLower();
   //         password = password?.Trim();

   //         if (!EmailHelper.IsValidEmail(email))
   //             throw new ArgumentException("Invalid Email.", nameof(email));

   //         if (!PasswordHelper.IsValidPassword(password))
   //             throw new ArgumentNullException(nameof(password));

   //         var hashedPassword = PasswordHelper.CreatePasswordHashed(password);

   //         User user = new User()
   //         {
   //             Email = email,
   //             Password = hashedPassword
   //         };

   //         _userRepository.Add(user);
   //          _userRepository.SaveChangeAsync();

			//var Datauser = _userRepository.GetByEmail(user.Email);
			//// _mapper.Map<UserDto>(user);
			////UserDto userDto = Datauser.select(us => new UserDto()
			////{
			////	Email= "dfdfd",
			////	Id=1				
			////});
			//UserDto userDto = new UserDto() { 
			//ErrorMessage="OK"
			//};

			//return userDto;
   //     }
		public string ForgotPassword(string email)
		{
			//string status = "";
			var user = _userRepository.GetByEmail(email);
			if (user != null)
			{
				string newpassword = RandomPassword();
				string newSalt = RandomCode();
				string hashPassword = HashSHA256(newpassword + newSalt);
				//var result = _userRepository.Update<User>(email, hashPassword, newSalt);
				//if (result == "Success")
				//{
				//	//senddata();
				//	return result;// newpassword;
				//}
				//else
				//{
					return "Update password fail";
				//}
			}
			else
			{
				return "No AccountEmail";
			}


		}
		private bool CheckUser(User user, string password)
		{
			string currentPassword = HashSHA256(password + user.Salt);
			return (user.Password == currentPassword) ? true : false;
		}
		private string HashSHA256(string psw)
		{
			SHA256 sHA256hash = SHA256.Create();
			byte[] bytes = sHA256hash.ComputeHash(Encoding.UTF8.GetBytes(psw));
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < bytes.Length; i++)
			{
				builder.Append(bytes[i].ToString("x2"));
			}
			string hashPSW = builder.ToString();
			return hashPSW;
		}
		public string RandomCode()
		{
			const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, random.Next(20, 25))
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
		public string RandomPassword()
		{
			const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, random.Next(8, 10))
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
		public bool UpdateUser(string email, string password)
		{
			string newSalt = RandomCode();
			string hashPassword = HashSHA256(password + newSalt);
			User result = _userRepository.GetByEmail(email);
			if (result != null)
			{
				result.Password = password;
			   _userRepository.Update<User>(result);
				return true;
			}
			else
			{
				return  false;
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
		private string createEmailBody(string userName, string message)
		{
			string body = string.Empty;
			//using (StreamReader reader = new StreamReader(HttpContext.MapPath("/htmlTemplate.html")))
			//{
			//	body = reader.ReadToEnd();
			//}
			body = body.Replace("{UserName}", userName);
			body = body.Replace("{message}", message);
			return body;
		}
		public UserDto GetDataUser(string email)
		{
			var user = _userRepository.GetByEmail(email);
			if (user == null)
			{
				return null;
			}
			UserDto userDto = new UserDto()
			{
				Id = user.Id,
			};
			return userDto;
		}

		public string RegisterUser(string email, string password)
		{
			//throw new NotImplementedException();
			var data = _userRepository.GetByEmail(email);
			if (data != null)
			{
				return "Email is exist!";
			}
			else
			{
				string newSalt = RandomCode();
				string newPassword = HashSHA256(password + newSalt);
				//var result = _userRepository.Add(email, newPassword, newSalt);
				User user = new User()
				{
					Email = email,
					Password = newPassword,
					FullName="dfddgd",
					About="ddgdgdgd",
					ActiveStatus=Enums.Status.Active,
					Salt="fsfsfsfsfs",
					RefeshToken="fsdfjghfghjk",
					AccessToken="fsdghjk"
				};

				_userRepository.Add(user);
				_userRepository.SaveChangeAsync();
				return "Success";
			}
			//throw new NotImplementedException();
		}
	}
}
