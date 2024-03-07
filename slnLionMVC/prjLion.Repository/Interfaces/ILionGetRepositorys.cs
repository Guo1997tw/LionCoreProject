using prjLion.Repository.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Interfaces
{
    public interface ILionGetRepositorys
    {
        /// <summary>
        /// 搜尋單一使用者留言
		/// 指定使用者姓名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<IEnumerable<MessageListDto>?> GetMsgByUserName(string userName);

        /// <summary>
        /// 登入帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Task<MemberAccountDto?> GetMemberAccount(string account);

        /// <summary>
        /// 分頁功能
        /// 輸入第幾頁
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public Task<IEnumerable<MessageListDto>> GetMsgPageNum(int pageNum);

        /// <summary>
        /// 取的留言版總筆數
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Task<int> GetMsgPageCount();
    }
}