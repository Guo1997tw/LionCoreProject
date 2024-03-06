using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

		/// <summary>
		/// 留言清單頁面
		/// </summary>
		/// <returns></returns>
		[Authorize]
        public IActionResult MsgList()
        {
            ViewBag.LoginAccount = _userAuthentication.GetUserName();

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

            return View();
        }
    }
}