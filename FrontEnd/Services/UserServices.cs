using FrontEnd.Models;
using Newtonsoft.Json;
using System.Text;

namespace FrontEnd.Services
{
    public interface IUser
    {
        Task<User> Register(User obj);
        Task<UserData> Login(Login obj);
        Task<UserData> GetUserName(string username);
    }
    public class UserServices : IUser
    {
        public async Task<UserData> GetUserName(string username)
        {
            UserData user = new UserData();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/User/{username}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<UserData>(apiResponse);
                    }
                }
            }
            return user;
        }

        public async Task<UserData> Login(Login obj)
        {
            UserData user = new();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:2024/api/Users/Login", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<UserData>(apiResponse);
                    }
                    else
                    {
                        throw new Exception("Login Gagal, Email atau Password Salah");
                    }
                }
            }
            return user;
        }

        public async Task<User> Register(User obj)
        {
            User user = new User();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:2024/api/User", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(apiResponse);
                    }
                }
            }
            return user;
        }
            }
}
