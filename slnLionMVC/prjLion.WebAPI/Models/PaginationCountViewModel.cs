﻿using prjLion.Service.Models.Bo;

namespace prjLion.WebAPI.Models
{
    public class PaginationCountViewModel<MessageListViewModel>
    {
        /// <summary>
        /// 裝分頁好的資料
        /// </summary>
        public IEnumerable<MessageListViewModel> ItemData { get; set; } = null!;

        /// <summary>
        /// 裝留言版資料總筆數
        /// </summary>
        public int CountData { get; set; }
    }
}