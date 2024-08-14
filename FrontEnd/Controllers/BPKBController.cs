using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                var viewModel = new updateBPKB();
                string myToken = string.Empty;
				if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
				{
					myToken = HttpContext.Session.GetString("token");
				}
                viewModel.objBPKB = await _bpkb.getByAgreementNum(agreementNum, myToken);
                var locations= await _bpkb.GETLocation(myToken);
                viewModel.Locations = locations.Select(loc => new SelectListItem
                {
                    Value = loc.LocationId.ToString(), // Sesuaikan dengan properti yang benar
                    Text = loc.LocationName // Sesuaikan dengan properti yang benar
                });
                return View(viewModel);
            }
			catch (Exception ex)
			{
				return View();
			}
		}
        [HttpPost]
        public async Task<IActionResult> UpdateData(updateBPKB obj)
        {
            try
            {
                

                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                var model = await _bpkb.UPDATE(obj.objBPKB, myToken);
                if (model.IsSucceeded)
                {
                    TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mengubah data</div>";
                }
                else
                {
                    TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Gagal mengubah data</div>";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
