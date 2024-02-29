using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjLionMVC.Interfaces;
using prjLionMVC.Models;
using prjLionMVC.Models.Entity;
using prjLionMVC.Models.ViewModels;
using System.Security.Claims;

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

		[HttpGet]
		public IEnumerable<MsgListViewModel> GetMsg()
		{
			return _lion.GetAllMsg().Select(x => new MsgListViewModel
			{
				MessageBoardId = x.MessageBoardId,
				Account = x.Account,
				MemberName = x.MemberName,
				MessageText = x.MessageText,
				MessageTime = x.MessageTime,
			}).ToList();
		}

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

		[HttpGet("{page}")]
		public IEnumerable<MsgListViewModel> GetChoosePageView(int page)
		{
			return _lion.GetMsgPage(page).Select(mv => new MsgListViewModel
			{
				MessageBoardId = mv.MessageBoardId,
				Account = mv.Account,
				MemberName = mv.MemberName,
				MessageText = mv.MessageText,
				MessageTime = mv.MessageTime,
			}).ToList();
		}

		[HttpPost]
		public bool RegisterMember(CreateAccountViewModel createAccountViewModel)
		{
			var mapper = new CreateAccountDto
			{
				MemberName = createAccountViewModel.MemberName,
				Account = createAccountViewModel.Account,
				HashPassword = createAccountViewModel.HashPassword,
			};

			return _lion.CreateMember(mapper) ? true : false;
		}

		[HttpPost]
		public bool LoginMember(LoginAccountViewModel loginAccountViewModel)
		{
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

		[HttpPost]
		public bool CreateMsg(CreateMsgViewModel createMsgViewModel)
		{
			var mapper = new CreateMsgDto
			{
				MemberId = createMsgViewModel.MemberId,
				MessageText = createMsgViewModel.MessageText,
				MessageTime = createMsgViewModel.MessageTime,
			};

			return _lion.InsertMsg(mapper) ? true : false;
		}

		[HttpPut("{id}")]
		public bool UpdateMsg(int id, EditMsgViewModel editMsgViewModel)
		{
			var mapper = new EditMsgDto
			{
				MessageText = editMsgViewModel.MessageText,
				MessageTime = editMsgViewModel.MessageTime,
			};

			return _lion.EditMsg(id, mapper);
		}

		[HttpDelete("{id}")]
		public bool RemoveMsg(int id)
		{
			return _lion.DeleteMsg(id);
		}
	}
}