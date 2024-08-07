using FrontEnd.Services;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FrontEnd.Controllers
{    
    public class UserController : Controller
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(string? returnUrl)
		{
			ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.returnUrl = returnUrl;
            }
            return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(Login obj)
		{
			try
			{                
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                UserData model = await _user.Login(obj);
                if (model != null)
                {
                    HttpContext.Session.SetString("token", $"Bearer {model.Token}");


                    List <Claim> claims = new();
                    claims.Add(new Claim("UserName", "test"));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>Wellcome</div>";
                    return RedirectToAction("Index","BPKB");
                }
                ViewData["pesan"] = $"<span class='alert alert-danger'>Login Gagal</span>";
                return View();
			}
			catch (Exception ex)
			{
				ViewData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>{ex.Message}</div>";
				return View();
			}
		}
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("Login");
        }
    }
}
