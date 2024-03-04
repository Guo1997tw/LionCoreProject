namespace prjLionMVC.Models
{
    public class LoginAccountDto
    {
		/// <summary>
		/// 登入帳號
		/// </summary>
		public string Account { get; set; } = null!;

		/// <summary>
		/// 登入密碼 (雜湊)
		/// </summary>
		public string HashPassword { get; set; } = null!;

		/// <summary>
		/// 登入密碼 (雜湊)
		/// </summary>
		public string SaltPassword { get; set; } = null!;
    }
}