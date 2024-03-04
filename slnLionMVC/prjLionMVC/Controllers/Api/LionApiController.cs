using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjLionMVC.Interfaces;
using prjLionMVC.Models;
using prjLionMVC.Models.Entity;
using prjLionMVC.Models.ViewModels;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace prjLionMVC.Controllers.Api
{
	[Route("api/LionApi/[action]")]
	[ApiController]
	public class LionApiController : ControllerBase
	{
		private readonly ILion _lion;

		public LionApiController(ILion lion)
		{
			_lion = lion;
		}
		/// <summary>
		/// 搜尋單一使用者留言
		/// 指定使用者姓名
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		[HttpGet("{userName}")]
		public IQueryable<MsgListViewModel> GetMemberOnlyMsg(string userName)
		{
			return _lion.GetMemberByNameMsg(userName).Select(x => new MsgListViewModel
			{
				MessageBoardId = x.MessageBoardId,
				Account = x.Account,
				MemberName = x.MemberName,
				MessageText = x.MessageText,
				MessageTime = x.MessageTime,
			});
		}

		/// <summary>
		/// 取得第幾頁
		/// 指定頁面 (分五筆)
		/// </summary>
		/// <param name="pageNum"></param>
		/// <returns></returns>
		[HttpGet("{pageNum}")]
		public IEnumerable<MsgListViewModel> GetPageNum(int pageNum)
		{
			return _lion.GetMsgPageNum(pageNum).Select(mv => new MsgListViewModel
			{
				MessageBoardId = mv.MessageBoardId,
				Account = mv.Account,
				MemberName = mv.MemberName,
				MessageText = mv.MessageText,
				MessageTime = mv.MessageTime,
			}).ToList();
		}

		/// <summary>
		/// 取得留言總筆數
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public int GetPageCount()
		{
			return _lion.GetMsgPageCount();
		}

		/// <summary>
		/// 註冊帳號
		/// </summary>
		/// <param name="createAccountViewModel"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		[HttpPost]
		public bool RegisterMember(CreateAccountViewModel createAccountViewModel)
		{
			var userNameRule = new Regex(@"^[a-zA-Z\u4e00-\u9fa5]+$");
            var accountRule = new Regex(@"^[A-Za-z0-9_]+$");
            var passwordRule = new Regex(@"^\S+$");

			if(!userNameRule.IsMatch(createAccountViewModel.MemberName))
			{
				throw new Exception("姓名欄位只能有中文、英文以及不允許有空格");
			}

            if (!accountRule.IsMatch(createAccountViewModel.Account))
            {
				throw new Exception("帳號欄位只能有字母、數字、底線");
            }

            if (!passwordRule.IsMatch(createAccountViewModel.HashPassword))
            {
				throw new Exception("密碼欄位不允許空格");
            }

            var mapper = new CreateAccountDto
			{
				MemberName = createAccountViewModel.MemberName,
				Account = createAccountViewModel.Account,
				HashPassword = createAccountViewModel.HashPassword,
			};

			return _lion.CreateMember(mapper) ? true : false;
		}

		/// <summary>
		/// 登入帳號
		/// </summary>
		/// <param name="loginAccountViewModel"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		[HttpPost]
		public bool LoginMember(LoginAccountViewModel loginAccountViewModel)
		{
            var accountRule = new Regex(@"^[A-Za-z0-9_]+$");
            var passwordRule = new Regex(@"^\S+$");

            if (!accountRule.IsMatch(loginAccountViewModel.Account))
            {
                throw new Exception("帳號欄位只能有字母、數字、底線");
            }

            if (!passwordRule.IsMatch(loginAccountViewModel.HashPassword))
            {
                throw new Exception("密碼欄位不允許空格");
            }

            if (_lion.CheckMember(loginAccountViewModel.Account, loginAccountViewModel.HashPassword))
			{
				var queryResult = _lion.GetMemberById(loginAccountViewModel.Account);

				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, $"{ queryResult.MemberId }"),
					new Claim(ClaimTypes.Name, $"{ queryResult.Account }")
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					principal, new AuthenticationProperties { ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60) });

				return true;
			}
			return false;
		}

		/// <summary>
		/// 取得單一使用者資訊
		/// 指定帳號
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		[HttpGet]
		public GetMemberViewModel GetOneMember(string account)
		{
			var queryResult = _lion.GetMemberById(account);

			return new GetMemberViewModel
			{
				MemberId = queryResult.MemberId,
				Account = queryResult.Account,
			};
		}

		/// <summary>
		/// 新增留言
		/// </summary>
		/// <param name="createMsgViewModel"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		[HttpPost]
		public bool CreateMsg(CreateMsgViewModel createMsgViewModel)
		{
			var msgRule = new Regex(@"^[a-zA-Z0-9\u4e00-\u9fa5，。、！？]+$");

			if (string.IsNullOrWhiteSpace(createMsgViewModel.MessageText))
			{
				throw new Exception("欄位不可為空");
			}

			if (!msgRule.IsMatch(createMsgViewModel.MessageText))
			{
				throw new Exception("留言欄位不允許有特殊字元");
			}

			var mapper = new CreateMsgDto
			{
				MemberId = createMsgViewModel.MemberId,
				MessageText = createMsgViewModel.MessageText,
				MessageTime = createMsgViewModel.MessageTime,
			};

			return _lion.InsertMsg(mapper) ? true : false;
		}

		/// <summary>
		/// 修改留言
		/// 指定留言編號 (流水號)
		/// </summary>
		/// <param name="id"></param>
		/// <param name="editMsgViewModel"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		public bool UpdateMsg(int id, EditMsgViewModel editMsgViewModel)
		{
			var msgRule = new Regex(@"^[a-zA-Z0-9\u4e00-\u9fa5，。、！？]+$");

			if (string.IsNullOrWhiteSpace(editMsgViewModel.MessageText))
			{
				throw new Exception("欄位不可為空");
			}

			if (!msgRule.IsMatch(editMsgViewModel.MessageText))
			{
				throw new Exception("留言欄位不允許有特殊字元");
			}

			var mapper = new EditMsgDto
			{
				MessageText = editMsgViewModel.MessageText,
				MessageTime = editMsgViewModel.MessageTime,
			};

			return _lion.EditMsg(id, mapper);
		}

		/// <summary>
		/// 刪除留言
		/// 指定留言編號 (流水號)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public bool RemoveMsg(int id)
		{
			return _lion.DeleteMsg(id);
		}
	}
}