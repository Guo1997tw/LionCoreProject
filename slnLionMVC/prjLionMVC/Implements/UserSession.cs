using prjLionMVC.Interfaces;

namespace prjLionMVC.Implements
{
	public class UserSession : IUserSession
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserSession(IHttpContextAccessor httpContextAccessor)
        {
			_httpContextAccessor = httpContextAccessor;
		}

		/// <summary>
		/// 取得Session使用者ID
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public int GetSessionCertificate()
		{
			var userIdValue = _httpContextAccessor.HttpContext.Session.GetString("MemberId");

			if(!string.IsNullOrEmpty(userIdValue) && int.TryParse(userIdValue, out int memberId)) return memberId;

			throw new InvalidOperationException("找不到此會員ID");
		}

		/// <summary>
		/// 取得Session使用者名稱
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public string GetSessionUserName()
		{
			var userNameValue = _httpContextAccessor.HttpContext.Session.GetString("Account");

			if(!string.IsNullOrEmpty(userNameValue)) return userNameValue;

			throw new InvalidOperationException("找不到此會員帳號");
		}
	}
}