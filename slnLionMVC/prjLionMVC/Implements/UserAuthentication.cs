using prjLionMVC.Interfaces;
using System.Security.Claims;

namespace prjLionMVC.Implements
{
    public class UserAuthentication : IUserAuthentication
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAuthentication(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

		/// <summary>
		/// 取得使用者識別碼
		/// 會員編號
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public int GetUserCertificate()
        {
            Claim userClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);

            if (userClaim != null && int.TryParse(userClaim.Value, out int id)) return id;

            throw new InvalidOperationException("找不到此會員ID");
        }

		/// <summary>
		/// 取得使用者名稱
		/// 會員帳號
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public string GetUserName()
		{
			Claim userClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name);

            if (userClaim != null) { return userClaim.Value; }

            throw new InvalidOperationException("找不到此會員帳號");
		}
	}
}