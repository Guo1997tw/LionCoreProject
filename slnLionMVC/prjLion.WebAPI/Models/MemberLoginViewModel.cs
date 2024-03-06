namespace prjLion.WebAPI.Models
{
	public class MemberLoginViewModel
	{
		/// <summary>
		/// 登入帳號
		/// </summary>
		public string Account { get; set; } = null!;

		/// <summary>
		/// 登入密碼 (雜湊)
		/// </summary>
		public string HashPassword { get; set; } = null!;
	}
}