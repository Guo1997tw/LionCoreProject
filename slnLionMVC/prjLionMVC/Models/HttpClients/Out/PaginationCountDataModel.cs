namespace prjLionMVC.Models.HttpClients.Out
{
    public class PaginationCountDataModel
    {
        /// <summary>
        /// 裝分頁好的資料
        /// </summary>
        public List<MessageListViewModel> itemData { get; set; } = null!;

        /// <summary>
        /// 裝留言版資料總筆數
        /// </summary>
        public int countData { get; set; }
    }
}