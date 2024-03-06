using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjLion.Common;
using prjLion.Service.Interfaces;
using prjLion.Service.Models.Bo;
using prjLion.WebAPI.Models;
using System.Security.Claims;

namespace prjLion.WebAPI.Controllers
{
    [Route("api/Lion/[action]")]
    [ApiController]
    public class LionController : ControllerBase
    {
        private readonly ILionGetServices _lionGetServices;
        private readonly ILionPostServices _lionPostServices;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public LionController(ILionGetServices lionGetServices, ILionPostServices lionPostServices, ITokenService tokenService, IMapper mapper)
        {
            _lionGetServices = lionGetServices;
            _lionPostServices = lionPostServices;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        /// <summary>
        /// 搜尋單一使用者留言
		/// 指定使用者姓名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("{userName}")]
        public async Task<ActionResult<IEnumerable<MessageListViewModel>?>> SearchMsgUserName(string userName)
        {
            var queryBo = await _lionGetServices.GetMsgByUserName(userName);

            if (queryBo == null || !queryBo.Any()) { return NotFound("無法搜尋到該筆使用者資料"); }

            return Ok(_mapper.Map<IEnumerable<MessageListViewModel>?>(queryBo));
        }

        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param name="memberAccountViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> RegisterMember(MemberAccountViewModel memberAccountViewModel)
        {
            var mapper = _mapper.Map<MemberAccountViewModel, MemberAccountBo>(memberAccountViewModel);

            return Ok(await _lionPostServices.CreateAccount(mapper));
        }

        /// <summary>
        /// 登入帳號
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost/*("{key}/{value}")*/]
        public async Task<bool> LoginMember(MemberLoginViewModel memberLoginViewModel)
        {
            CustomizedMethod customizedMethod = new CustomizedMethod();

            customizedMethod.isVerifyRuleAP(memberLoginViewModel.Account, memberLoginViewModel.HashPassword);

            if(await _lionPostServices.CheckMember(memberLoginViewModel.Account, memberLoginViewModel.HashPassword))
            {
                var queryResult = await _lionGetServices.GetMemberInfo(memberLoginViewModel.Account);

				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, $"{ queryResult.MemberId }"),
					new Claim(ClaimTypes.Name, $"{ queryResult.Account }")
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					principal, new AuthenticationProperties { ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60) });

                return true;
			}

            return false;

            //if (await _lionPostServices.CheckMember(key, value))
            //{
            //    var queryResult = await _lionGetServices.GetMemberInfo(key);

            //    var claims = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.NameIdentifier, $"{ queryResult.MemberId }"),
            //        new Claim(ClaimTypes.Name, $"{ queryResult.MemberName }")
            //    };

            //    var (Token, Expiration) = _tokenService.CreateToken(claims);

            //    return Ok(new
            //    {
            //        token = Token,
            //        expiration = Expiration
            //    });
            //}

            //return NotFound("登入失敗");
        }
    }
}