using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using prjLionMVC.Interfaces;
using prjLionMVC.Models.HttpClients.Inp;
using System.Security.Claims;

namespace prjLionMVC.Implements
{
    public class HttpClientlogics : IHttpClientlogics
    {
        private readonly IHttpClients _httpClients;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientlogics(IHttpClients httpClients, IHttpContextAccessor httpContextAccessor)
        {
            _httpClients = httpClients;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 登入成功將設置Cookie
        /// 60分鐘憑證
        /// </summary>
        /// <param name="loginMemberViewModel"></param>
        /// <returns></returns>
        public async Task<string> IsIdentityCheckAsync(LoginMemberViewModel loginMemberViewModel)
        {
            var result = await _httpClients.LoginPostAsync(loginMemberViewModel);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return ("false");
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, $"{ result.data.memberId }"),
                    new Claim(ClaimTypes.Name, $"{ result.data.account }")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, new AuthenticationProperties { ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60) });

                _httpContextAccessor.HttpContext.Session.SetString("MemberId", result.data.memberId.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("Account", result.data.account.ToString());

                var r = _httpContextAccessor.HttpContext.Session.Id;

                Console.WriteLine(r);

                return ("true");
            }
        }
    }
}