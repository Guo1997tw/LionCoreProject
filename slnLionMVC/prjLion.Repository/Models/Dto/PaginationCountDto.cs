using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Models.Dto
{
    public class PaginationCountDto<MessageListDto>
    {
        /// <summary>
        /// 裝分頁好的資料
        /// </summary>
        public IEnumerable<MessageListDto> ItemData { get; set; } = null!;

        /// <summary>
        /// 裝留言版資料總筆數
        /// </summary>
        public int CountData { get; set; }
    }
}