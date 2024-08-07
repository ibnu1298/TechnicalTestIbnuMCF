using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class User
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class Login
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
    public class UserData
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
