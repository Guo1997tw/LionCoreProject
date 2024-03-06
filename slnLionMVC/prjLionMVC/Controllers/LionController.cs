using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using prjLion.Service.Interfaces;
using prjLionMVC.Interfaces;
using prjLionMVC.Models;

namespace prjLionMVC.Controllers
{
    public class LionController : Controller
    {
		private readonly IUserAuthentication _userAuthentication;
		private readonly IAuthenticationServices _authenticationServices;

		public LionController(IUserAuthentication userAuthentication, IAuthenticationServices authenticationServices)
        {
			_userAuthentication = userAuthentication;
			_authenticationServices = authenticationServices;
		}

		/// <summary>
		/// 留言清單頁面
		/// </summary>
		/// <returns></returns>
		[Authorize]
        public IActionResult MsgList()
        {
            // ViewBag.LoginAccount = _userAuthentication.GetUserName();

			ViewBag.LoginAccount = _authenticationServices.GetUserName();

			return View();
        }

		/// <summary>
		/// 註冊帳號頁面
		/// </summary>
		/// <returns></returns>
		public IActionResult Register()
        {
            return View();
        }

		/// <summary>
		/// 登入帳號頁面
		/// </summary>
		/// <returns></returns>
		public IActionResult Login()
        {
            return View();
        }
		[HttpPost]
		public IActionResult Login(string account, string hashPassword)
		{
			return View();
		}

		/// <summary>
		/// 登出帳號頁面
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Lion");
        }

        /// <summary>
        /// 顯示沒有權限頁面
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View();
        }

		/// <summary>
		/// 新增留言頁面
		/// </summary>
		/// <returns></returns>
		public IActionResult UseMsg()
        {
			// ViewBag.MemberId = _userAuthentication.GetUserCertificate();

			ViewBag.MemberId = _authenticationServices.GetUserCertificate();

			return View();
        }
    }
}