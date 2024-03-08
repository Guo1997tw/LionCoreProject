using prjLion.Repository.Models.Dto;
using prjLion.Service.Models.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Service.Interfaces
{
    public interface ILionGetServices
    {
        /// <summary>
        /// 搜尋單一使用者留言
		/// 指定使用者姓名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<IEnumerable<MessageListBo>?> GetMsgByUserName(string userName);

        /// <summary>
        /// 分頁功能
        /// 輸入第幾頁
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public Task<IEnumerable<MessageListBo>> GetMsgPage(int pageNum);

        /// <summary>
        /// 取的留言版總筆數
        /// </summary>
        /// <returns></returns>
        public Task<int> GetMsgCount();

        /// <summary>
        /// 同時取得資料分頁與總筆數
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public Task<PaginationCountBo<MessageListBo>> GetPaginationCountData(int pageNum);
    }
}