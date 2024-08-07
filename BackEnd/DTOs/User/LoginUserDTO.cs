namespace BackEnd.DTOs.User
{
    public class LoginUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UserDTO : BaseResponse
    {
        public string Token { get; set; }
    }
}
