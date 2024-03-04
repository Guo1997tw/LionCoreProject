namespace prjLionMVC.Models
{
    public class GetMemberDto
    {
		/// <summary>
		/// 會員編號
		/// 流水號
		/// </summary>
		public int MemberId { get; set; }

		/// <summary>
		/// 登入帳號
		/// </summary>
		public string Account { get; set; } = null!;
    }
}