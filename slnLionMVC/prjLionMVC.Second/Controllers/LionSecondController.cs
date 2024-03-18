using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace prjLionMVC.Second.Controllers
{
	public class LionSecondController : Controller
	{
        private readonly IDistributedCache _distributedCache;

        public LionSecondController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> CheckSession(string MemberId, string Account)
		{
            var cacheMemberId = $"MemberId_{MemberId}";
            var cacheAccount = $"Account_{Account}";

            var memberId = await _distributedCache.GetStringAsync(cacheMemberId);
            var account = await _distributedCache.GetStringAsync(cacheAccount);

            ViewBag.MemberId = memberId;
            ViewBag.Account = account;

            return View();
		}
	}
}