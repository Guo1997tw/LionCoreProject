namespace prjLionMVC.Models.HttpClients.Out
{
    public class MessageListViewModel
    {
        /// <summary>
		/// 留言編號
		/// 流水號
		/// </summary>
		public int messageBoardId { get; set; }

        /// <summary>
        /// 會員姓名
        /// </summary>
        public string memberName { get; set; } = null!;

        /// <summary>
        /// 會員帳號
        /// </summary>
        public string account { get; set; } = null!;

        /// <summary>
        /// 留言內容
        /// </summary>
        public string messageText { get; set; } = null!;

        /// <summary>
        /// 留言時間
        /// 時間戳
        /// </summary>
        public DateTime messageTime { get; set; }
    }
}