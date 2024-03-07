namespace prjLionMVC.Models.HttpClients.Inp
{
	public class RegisterMemberViewModel
	{
		/// <summary>
		/// 會員姓名
		/// </summary>
		public string MemberName { get; set; } = null!;
		
		/// <summary>
		/// 會員帳號
		/// </summary>
		public string Account { get; set; } = null!;
		

		/// <summary>
		/// 會員密碼
		/// </summary>
		public string HashPassword { get; set; } = null!;
	}
}