using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Win32;
using prjLionMVC.Interfaces;
using prjLionMVC.Models.HttpClients;
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
        private readonly IHttpClients _httpClients;
        private readonly IHttpClientlogics _httpClientlogics;

        public LionController(IHttpClientFactory httpClientFactory, IUserAuthentication userAuthentication, IHttpClients httpClients, IHttpClients _httpClients1, IHttpClientlogics httpClientlogics)
        {
            _httpClientFactory = httpClientFactory;
            _userAuthentication = userAuthentication;
            _httpClients = httpClients;
            _httpClientlogics = httpClientlogics;
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
        /// 同時取得資料分頁與總筆數
        /// 指定頁數
        /// </summary>
        /// <param name="currentShowPage"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResultTOutputDataViewModel<PaginationCountDataViewModel>>> GetDataCountAll([FromForm] int currentShowPage)
        {
            var result = await _httpClients.MsgPageAllPostAsync(currentShowPage);

            return new ResultTOutputDataViewModel<PaginationCountDataViewModel>
			{
                success = true,
                message = "載入成功",
                data = result.data,
            };
        }

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="currentShowPage"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResultTOutputDataViewModel<PaginationCountDataViewModel>>> SearchMsgUserNameDataCountAll([FromForm] string userName, int currentShowPage)
        {
            var result = await _httpClients.SearchMsgUserPostAsync(userName, currentShowPage);

			return new ResultTOutputDataViewModel<PaginationCountDataViewModel>
			{
				success = true,
				message = "載入成功",
				data = result.data,
			};
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
        public async Task<ActionResult<ResultTOutputDataViewModel<PaginationCountDataViewModel>>> RegisterPost([FromBody] RegisterMemberViewModel registerMemberViewModel)
        {
            return await _httpClients.RegisterPostAsync(registerMemberViewModel);
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
            var result = await _httpClientlogics.IsIdentityCheckAsync(loginMemberInputViewModel);

            return (result != "false") ? Content(result, "application/json") : Json(false);
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
        [HttpPost]
        public async Task<IActionResult> UseMsgPost([FromBody] InsertMsgViewModel insertMsgViewModel)
        {
            var result = await _httpClients.UseMsgPostAsync(insertMsgViewModel);

            return (result != false) ? Ok(true) : BadRequest(false);
        }

        /// <summary>
        /// 編輯留言頁面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editMsgViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> EditMsgPost(int id, [FromBody] EditMsgViewModel editMsgViewModel)
        {
            var result = await _httpClients.EditMsgPostAsync(id, editMsgViewModel);

            return (result != false) ? Ok(true) : BadRequest(false);
        }

        /// <summary>
        /// 刪除留言
        /// 指定留言編號 (流水號)
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> RemoveMsgPost(int id)
        {
            var result = await _httpClients.RemoveMsgPostAsync(id);

            return result ? Ok(true) : BadRequest(false);
        }
    }
}