using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace FrontEnd.Controllers
{
    public class BPKBController : Controller
    {
		private readonly IBpkb _bpkb;

		public BPKBController(IBpkb bpkb)
		{
			_bpkb = bpkb;
		}
		public async Task<IActionResult> Index()
        {
			ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "User");
			}
			IEnumerable<BPKB> results;
			string myToken = string.Empty;
			myToken = HttpContext.Session.GetString("token");
			if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
			{
				myToken = HttpContext.Session.GetString("token");
			}
			results = await _bpkb.GET(myToken);

			return View(results);
        }
        [HttpGet("BPKB/UpdateData/{agreementNum}")]
        public async Task<IActionResult> UpdateData(string agreementNum) 
		{
			try
			{
				string myToken = string.Empty;
				if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
				{
					myToken = HttpContext.Session.GetString("token");
				}
				var model = await _bpkb.getByAgreementNum(agreementNum, myToken);
                return View(model);
            }
			catch (Exception ex)
			{
				return View();
			}
		}
        [HttpPost]
        public async Task<IActionResult> UpdateData(BPKB obj)
        {
            try
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                var model = await _bpkb.UPDATE(obj, myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mengubah data</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
