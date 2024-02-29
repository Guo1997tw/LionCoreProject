using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using prjLionMVC.Interfaces;

namespace prjLionMVC.Controllers
{
    public class LionController : Controller
    {
		private readonly IUserAuthentication _userAuthentication;

		public LionController(IUserAuthentication userAuthentication)
        {
			_userAuthentication = userAuthentication;
		}

        public IActionResult MsgList()
        {
            ViewBag.LoginAccount = _userAuthentication.GetUserName();

			return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Lion");
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult UseMsg()
        {
            ViewBag.MemberId = _userAuthentication.GetUserCertificate();

            return View();
        }
    }
}