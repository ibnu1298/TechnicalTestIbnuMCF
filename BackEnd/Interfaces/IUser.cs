using BackEnd.DTOs.User;
using BackEnd.Models;

namespace BackEnd.Interfaces
{
    public interface IUser : ICrud<MsUser>
    {
        Task<UserDTO> Authenticate(LoginUserDTO request);
    }
}
