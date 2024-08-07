using BackEnd.DTOs;
using BackEnd.DTOs.User;
using BackEnd.Helpers;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Services
{
    public class UserFacade : IUser
    {
        private readonly DataContext _context;
        private AppSettings _appSettings;
        public UserFacade(DataContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<UserDTO> Authenticate(LoginUserDTO request)
        {
            UserDTO response = new();
            var checkUser = await _context.MsUsers.FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Password == request.Password);
            if (checkUser == null)
            {
                response.IsSucceeded = false;
                response.Message =  "Autentikasi gagal!, Email atau Password Salah";
                return response;
            }
            if (!checkUser.IsActive)
            {
                response.IsSucceeded = false;
                response.Message = "User is no longer active";
                return response;                
            }
            JwtSecurityToken token = new JwtSecurityToken(                
                expires: DateTime.UtcNow.AddDays(1),
                claims: new[] 
                { 
                    new Claim("UserId", checkUser.UserId.ToString()),
                    new Claim("UserName", checkUser.UserName),
                },
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret)),
                    algorithm: SecurityAlgorithms.HmacSha256
                )
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            response.Token = tokenHandler.WriteToken(token);
            response.IsSucceeded = true;
            response.Message = "Authentication Successful";
            return response;
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MsUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<MsUser> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MsUser>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> Insert(MsUser obj)
        {
            try
            {
                BaseResponse response = new();
                var newUser = new InsertUpdateDTO
                {
                    UserName = obj.UserName,
                    Password = obj.Password,
                };
                var email = await _context.MsUsers.FirstOrDefaultAsync(x => x.UserName == obj.UserName);
                if (email != null)
                {
                    response.IsSucceeded = false;
                    response.Message = $"Username {obj.UserName} already taken";
                    return response;
                }

                var insertData = _context.MsUsers.Add(obj);
                await _context.SaveChangesAsync();
                response.IsSucceeded = true;
                response.Message = $"User Successfully Registered";
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public Task<BaseResponse> Update(MsUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
