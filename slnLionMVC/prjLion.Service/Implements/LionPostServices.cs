using AutoMapper;
using prjLion.Common;
using prjLion.Repository.Interfaces;
using prjLion.Repository.Models.Dto;
using prjLion.Service.Interfaces;
using prjLion.Service.Models.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prjLion.Service.Implements
{
    public class LionPostServices : ILionPostServices
    {
		private readonly ILionGetRepositorys _lionGetRepositorys;
		private readonly ILionPostRepositorys _lionPostRepositorys;
        private readonly IMapper _mapper;

        public LionPostServices(ILionGetRepositorys lionGetRepositorys, ILionPostRepositorys lionPostRepositorys, IMapper mapper)
        {
			_lionGetRepositorys = lionGetRepositorys;
			_lionPostRepositorys = lionPostRepositorys;
            _mapper = mapper;
        }

		/// <summary>
		/// 註冊帳號
		/// </summary>
		/// <param name="memberAccountBo"></param>
		/// <returns></returns>
		public async Task<bool> CreateAccount(MemberAccountBo memberAccountBo)
        {
            var userNameRule = new Regex(@"^[a-zA-Z\u4e00-\u9fa5]+$");

            if (!userNameRule.IsMatch(memberAccountBo.MemberName))
            {
                throw new Exception("姓名欄位只能有中文、英文以及不允許有空格");
            }

            CustomizedMethod customizedMethod = new CustomizedMethod();

            customizedMethod.isVerifyRuleAP(memberAccountBo.Account, memberAccountBo.HashPassword);

            var salt = customizedMethod.RandomSalt();
            var hasPwd = customizedMethod.HashPwdWithHMACSHA256(memberAccountBo.HashPassword, salt);

            memberAccountBo.HashPassword = hasPwd;
            memberAccountBo.SaltPassword = salt;

            var mapper = _mapper.Map<MemberAccountBo, MemberAccountDto>(memberAccountBo);

            return await _lionPostRepositorys.InsertAccount(mapper);
        }

        /// <summary>
        /// 登入帳號
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
		public async Task<MemberAccountBo?> CheckMember(string account, string password)
		{
            var queryResult = await _lionGetRepositorys.GetMemberAccount(account);
			
            CustomizedMethod customizedMethod = new CustomizedMethod();
			
            customizedMethod.isVerifyRuleAP(account, password);

            if (queryResult != null)
            {
                var HashPasswordTemp = queryResult.HashPassword;
                var SaltPasswordTemp = queryResult.SaltPassword;
                var HashPassword = customizedMethod.HashPwdWithHMACSHA256(password, SaltPasswordTemp);

                // return HashPassword == HashPasswordTemp;
            }

            return _mapper.Map<MemberAccountBo>(queryResult);
		}
	}
}