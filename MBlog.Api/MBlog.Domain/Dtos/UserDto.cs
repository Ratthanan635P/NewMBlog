namespace MBlog.Domain.Dtos
{
    public class UserDto : BaseDto
    {
        public string AccessToken { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string About { get; set; }
        public byte[] ImageProfile { get; set; }
        public string ImageProfilePath { get; set; }
    }
}
