﻿namespace prjLionMVC.Models.HttpClients.Out
{
	/// <summary>
	/// 反序列化資料都需要與JSON格式一樣小寫
	/// </summary>
	public class LoginInfoViewModel
	{
		/// <summary>
		/// 會員編號
		/// </summary>
		public int memberId { get; set; }

		/// <summary>
		/// 會員帳號
		/// </summary>
		public string account { get; set; } = null!;
	}
}