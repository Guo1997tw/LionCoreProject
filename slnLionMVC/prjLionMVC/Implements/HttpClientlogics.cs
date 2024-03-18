using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using prjLionMVC.Interfaces;
using prjLionMVC.Models.HttpClients.Inp;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Distributed;

namespace prjLionMVC.Implements
{
    public class HttpClientlogics : IHttpClientlogics
    {
        private readonly IHttpClients _httpClients;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _distributedCache;

        public HttpClientlogics(IHttpClients httpClients, IHttpContextAccessor httpContextAccessor, IDistributedCache distributedCache)
        {
            _httpClients = httpClients;
            _httpContextAccessor = httpContextAccessor;
            _distributedCache = distributedCache;
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

                var cacheMemberId = $"MemberId_{result.data.memberId}";
                var cacheAccount = $"Account_{result.data.account}";

                await _distributedCache.SetStringAsync(cacheMemberId, result.data.memberId.ToString(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
                });

                Console.WriteLine(cacheMemberId);

                await _distributedCache.SetStringAsync(cacheAccount, result.data.account.ToString(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
                });

                Console.WriteLine(cacheAccount);

                return ("true");
            }
        }
    }
}