namespace prjLionMVC.Models.HttpClients.Inp
{
	public class InsertMsgViewModel
	{
		/// <summary>
		/// 會員編號
		/// </summary>
		public string MemberId { get; set; } = null!;
		
		/// <summary>
		/// 留言內容
		/// </summary>
		public string MessageText { get; set; } = null!;
	}
}