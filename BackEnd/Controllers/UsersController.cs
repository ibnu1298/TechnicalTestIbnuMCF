using AutoMapper;
using BackEnd.DTOs.User;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public UsersController(IUser user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Authenticate(LoginUserDTO request)
        {
            try
            {
                var user = await _user.Authenticate(request);
                if (!user.IsSucceeded) 
                {
                    return BadRequest(user);
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
