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
    }
}