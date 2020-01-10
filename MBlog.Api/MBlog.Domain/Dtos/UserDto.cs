namespace MBlog.Domain.Dtos
{
    public class UserDto : BaseDto
    {
        public string ErrorMessage { get; set; }
        public string AccessToken { get; set; }
        public string Email { get; set; }
    }
}
