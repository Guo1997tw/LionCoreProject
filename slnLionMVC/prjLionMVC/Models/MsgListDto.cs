namespace prjLionMVC.Models
{
    public class MsgListDto
    {
		/// <summary>
		/// 留言編號
		/// 流水號
		/// </summary>
		public int MessageBoardId { get; set; }

		/// <summary>
		/// 會員姓名
		/// </summary>
		public string MemberName { get; set; } = null!;

		/// <summary>
		/// 會員帳號
		/// </summary>
		public string Account { get; set; } = null!;

		/// <summary>
		/// 留言內容
		/// </summary>
		public string MessageText { get; set; } = null!;

		/// <summary>
		/// 留言時間
		/// 時間戳
		/// </summary>
		public DateTime MessageTime { get; set; }
    }
}