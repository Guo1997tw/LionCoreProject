using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using prjLionMVC.Interfaces;
using prjLionMVC.Models.HttpClients.Inp;
using prjLionMVC.Models.HttpClients.Out;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace prjLionMVC.Controllers
{
    public class LionController : Controller
    {
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IUserAuthentication _userAuthentication;

		public LionController(IHttpClientFactory httpClientFactory, IUserAuthentication userAuthentication)
        {
			_httpClientFactory = httpClientFactory;
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
		[HttpPost]
		public async Task<IActionResult> RegisterPost([FromBody] RegisterMemberViewModel registerMemberViewModel)
		{
			var client = _httpClientFactory.CreateClient();

			var json = JsonSerializer.Serialize(registerMemberViewModel);

			var content = new StringContent(json, Encoding.UTF8, "application/json");

			try
			{
				var response = await client.PostAsync("https://localhost:7235/api/Lion/RegisterMember", content);

				return (response.IsSuccessStatusCode) ? Json(true) : Json(false);
			}
			catch (HttpRequestException)
			{
				return Json(false);
			}
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
		public async Task<IActionResult> LoginPost([FromBody] LoginMemberViewModel loginMemberInputViewModel)
		{
			// 建立連線
			var client = _httpClientFactory.CreateClient();

			// 序列化
			var json = JsonSerializer.Serialize(loginMemberInputViewModel);

			// 指定ContentType
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			try
			{
				var response = await client.PostAsync("https://localhost:7235/api/Lion/LoginMember", content);

				if(response.IsSuccessStatusCode)
				{
					// 讀取資料
					var responseContent = await response.Content.ReadAsStringAsync();

					// 反序列化
					var queryResult = JsonSerializer.Deserialize<LoginInfoViewModel>(responseContent);

					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.NameIdentifier, $"{ queryResult.MemberId }"),
						new Claim(ClaimTypes.Name, $"{ queryResult.Account }")
					};

					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var principal = new ClaimsPrincipal(identity);

					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
						principal, new AuthenticationProperties { ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60) });

					return Json(true);
				}
				else
				{
					ModelState.AddModelError(string.Empty, "登入失敗");

					return Json(false);
				}
			}
			catch (HttpRequestException)
			{
				return Json(false);
			}
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
            ViewBag.MemberId = _userAuthentication.GetUserCertificate();

            return View();
        }
    }
}