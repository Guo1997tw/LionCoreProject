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

        public int GetUserCertificate()
        {
            Claim userClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);

            if (userClaim != null && int.TryParse(userClaim.Value, out int id)) return id;

            throw new InvalidOperationException("找不到此會員ID");
        }

		public string GetUserName()
		{
			Claim userClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name);

            if (userClaim != null) { return userClaim.Value; }

            throw new InvalidOperationException("找不到此會員帳號");

            return null;
		}
	}
}