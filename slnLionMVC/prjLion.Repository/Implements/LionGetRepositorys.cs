using Dapper;
using prjLion.Repository.Helpers;
using prjLion.Repository.Interfaces;
using prjLion.Repository.Models.Dto;

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
        /// 同時取得資料分頁與總筆數
        /// pageNum -> 輸入第幾頁
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PaginationCountDto<MessageListDto>?> GetPaginationCount(int pageNum)
        {
            int pageNow = 0, pageSize = 5;

            if (pageNum > 0) pageNow = (pageNum - 1) * pageSize;

            using (var use = _lionConnection.GetLionDb())
            {
                var paginationCountResult = new PaginationCountDto<MessageListDto>();

                var queryDataSQL = @"select mb.MessageBoardId, m.MemberName, m.Account, mb.MessageText, mb.MessageTime
                             from MessageBoardTable as mb
                             inner join MemberTable as m on mb.MemberId = m.MemberId
                             order by mb.MessageTime DESC
                             offset @PageNow rows fetch next @PageSize rows only;";

                var queryCountSQL = @"select count(*) from MessageBoardTable";

                paginationCountResult.ItemData = await use.QueryAsync<MessageListDto>(queryDataSQL, new { PageNow = pageNow, PageSize = pageSize });
                paginationCountResult.CountData = await use.ExecuteScalarAsync<int>(queryCountSQL);

                return paginationCountResult;
            }
        }

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public async Task<PaginationCountDto<MessageListDto>?> GetMsgByUserNamePaginationCount(string userName, int pageNum)
        {
            int pageNow = 0;
            int pageSize = 5;

            if (pageNum > 0) { pageNow = (pageNum - 1) * pageSize; }

            using (var use = _lionConnection.GetLionDb())
            {
                var paginationCountResult = new PaginationCountDto<MessageListDto>();

                var queryDataSQL = @"select mb.MessageBoardId, m.MemberName, m.Account, mb.MessageText, mb.MessageTime
                                     from MessageBoardTable as mb
                                     inner join MemberTable as m on mb.MemberId = m.MemberId
				                     where m.MemberName = @MemberName
                                     order by mb.MessageTime DESC
                                     offset @PageNow rows fetch next @PageSize rows only;";

                var queryCountSQL = @"select count(*)
                                      from MessageBoardTable as mb
                                      inner join MemberTable as m on mb.MemberId = m.MemberId
                                      where m.MemberName = @MemberName";

                paginationCountResult.ItemData = await use.QueryAsync<MessageListDto>(queryDataSQL, new { MemberName = userName, PageNow = pageNow, PageSize = pageSize });
                paginationCountResult.CountData = await use.ExecuteScalarAsync<int>(queryCountSQL, new { MemberName = userName });

                return paginationCountResult;
            }
        }
    }
}