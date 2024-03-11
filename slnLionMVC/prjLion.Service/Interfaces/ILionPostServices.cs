using prjLion.Service.Models.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Service.Interfaces
{
    public interface ILionPostServices
    {
        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param name="memberAccountBo"></param>
        /// <returns></returns>
        public Task<bool> CreateAccount(MemberAccountBo memberAccountBo);

        /// <summary>
        /// 登入帳號
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<bool> CheckMember(string account, string password);

		/// <summary>
		/// 新增留言
		/// </summary>
		/// <param name="createMsgBo"></param>
		/// <returns></returns>
		public Task<bool> CreateMsg(CreateMsgBo createMsgBo);

        /// <summary>
        /// 修改留言
		/// 指定留言編號 (流水號)
        /// </summary>
        /// <param name="editMsgBo"></param>
        /// <returns></returns>
        public Task<bool> EditMsg(int id, EditMsgBo editMsgBo);

		/// <summary>
		/// 刪除留言
		/// 指定留言編號 (流水號)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<bool> DeleteMemberMsg(int id);
	}
}