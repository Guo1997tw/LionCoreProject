using prjLion.Repository.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Interfaces
{
    public interface ILionPostRepositorys
    {
        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param name="memberAccountDto"></param>
        /// <returns></returns>
        public Task<bool> InsertAccount(MemberAccountDto memberAccountDto);
    }
}