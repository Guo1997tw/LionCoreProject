using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace prjLionMVC.CustomizeFilters
{
	public class SessionToViewBagFilter : IActionFilter
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SessionToViewBagFilter(IHttpContextAccessor httpContextAccessor)
        {
			_httpContextAccessor = httpContextAccessor;
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			var controller = context.Controller as Controller;

			if (controller != null)
			{
				controller.ViewBag.conMemberId = _httpContextAccessor.HttpContext.Session.GetString("MemberId");
				controller.ViewBag.conAccount = _httpContextAccessor.HttpContext.Session.GetString("Account");
			}
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			
		}
	}
}