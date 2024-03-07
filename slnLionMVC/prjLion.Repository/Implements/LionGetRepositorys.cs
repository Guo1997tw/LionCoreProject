using Dapper;
using prjLion.Repository.Helpers;
using prjLion.Repository.Interfaces;
using prjLion.Repository.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Implements
{
    public class LionGetRepositorys : ILionGetRepositorys
    {
        private readonly ILionConnection _lionConnection;

        public LionGetRepositorys(ILionConnection lionConnection)
        {
            _lionConnection = lionConnection;
        }

		/// <summary>
		/// 搜尋單一使用者留言
		/// 指定使用者姓名
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<IEnumerable<MessageListDto>?> GetMsgByUserName(string userName)
        {
            using (var use = _lionConnection.GetLionDb())
            {
                var querySQL = @"select [mb].[MessageBoardId], [m].[MemberName], [m].[Account], [mb].[MessageText], [mb].[MessageTime]
                                 from [dbo].[MessageBoardTable] as mb
                                 inner join [dbo].[MemberTable] as m on mb.MemberId = m.MemberId
                                 where m.MemberName = @MemberName";

                if (querySQL == null) { return null; }

                return await use.QueryAsync<MessageListDto>(querySQL, new { MemberName = userName });
            }
        }

        /// <summary>
        /// 登入帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
		public async Task<MemberAccountDto?> GetMemberAccount(string account)
		{
			using (var use = _lionConnection.GetLionDb())
			{
				var querySQL = @"select * from [dbo].[MemberTable] where [Account] = @account";

				return await use.QueryFirstOrDefaultAsync<MemberAccountDto?>(querySQL, new { Account = account });
			}
		}

        /// <summary>
        /// 分頁功能
        /// 輸入第幾頁
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<MessageListDto>> GetMsgPageNum(int pageNum)
        {
            int pageNow = 0;
            int pageSize = 5;

            if (pageNum > 0) { pageNow = (pageNum - 1) * pageSize; }

            using (var use = _lionConnection.GetLionDb())
            {
                var querySQL = @"select mb.MessageBoardId, m.MemberName, m.Account, mb.MessageText, mb.MessageTime
                             from MessageBoardTable as mb
                             inner join MemberTable as m on mb.MemberId = m.MemberId
                             order by mb.MessageBoardId
                             offset @PageNow rows fetch next @PageSize rows only;";

                return await use.QueryAsync<MessageListDto>(querySQL, new { PageNow = pageNow, PageSize = pageSize });
            }
        }

        /// <summary>
        /// 取的留言版總筆數
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> GetMsgPageCount()
        {
            using (var use = _lionConnection.GetLionDb())
            {
                var querySQL = @"select count(*) from MessageBoardTable";

                int dataCount = await use.ExecuteScalarAsync<int>(querySQL);

                return dataCount;
            }
        }
    }
}