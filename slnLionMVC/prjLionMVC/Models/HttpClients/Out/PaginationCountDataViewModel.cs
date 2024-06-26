﻿namespace prjLionMVC.Models.HttpClients.Out
{
    public class PaginationCountDataViewModel
    {
        /// <summary>
        /// 裝分頁好的資料
        /// </summary>
        public List<MessageListViewModel> ItemData { get; set; } = null!;

        /// <summary>
        /// 裝留言版資料總筆數
        /// </summary>
        public int CountData { get; set; }
    }
}