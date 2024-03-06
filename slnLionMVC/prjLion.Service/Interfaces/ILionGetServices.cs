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
        /// 帳號相關資訊
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Task<MemberAccountBo?> GetMemberInfo(string account);
    }
}