namespace prjLion.WebAPI.Models
{
	public class CreateMsgViewModel
	{
		/// <summary>
		/// 會員編號
		/// 流水號
		/// </summary>
		public int MemberId { get; set; }

		/// <summary>
		/// 留言內容
		/// </summary>
		public string MessageText { get; set; } = null!;

		/// <summary>
		/// 留言時間
		/// 時間戳
		/// </summary>
		public DateTime MessageTime { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"));
	}
}