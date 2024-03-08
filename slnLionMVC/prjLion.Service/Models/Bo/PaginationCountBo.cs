using prjLion.Repository.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Service.Models.Bo
{
    public class PaginationCountBo<MessageListBo>
    {
        /// <summary>
        /// 裝分頁好的資料
        /// </summary>
        public IEnumerable<MessageListBo> ItemData { get; set; } = null!;

        /// <summary>
        /// 裝留言版資料總筆數
        /// </summary>
        public int CountData { get; set; }
    }
}