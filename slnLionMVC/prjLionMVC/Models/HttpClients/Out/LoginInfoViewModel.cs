namespace prjLionMVC.Models.HttpClients.Out
{
	/// <summary>
	/// 反序列化資料都需要與JSON格式一樣小寫
	/// </summary>
	public class LoginInfoViewModel
	{
		public int memberId { get; set; }
		public string account { get; set; } = null!;
		public string hashPassword { get; set; } = null!;
	}
}