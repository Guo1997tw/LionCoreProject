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
	}
}