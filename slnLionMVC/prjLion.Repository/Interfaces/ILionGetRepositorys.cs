using prjLion.Repository.Models.Dto;

namespace prjLion.Repository.Interfaces
{
    public interface ILionGetRepositorys
    {
        /// <summary>
        /// 登入帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Task<MemberAccountDto?> GetMemberAccount(string account);

        /// <summary>
        /// 同時取得資料分頁與總筆數
        /// 指定頁數
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public Task<PaginationCountDto<MessageListDto>?> GetPaginationCount(int pageNum);

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public Task<PaginationCountDto<MessageListDto>?> GetMsgByUserNamePaginationCount(string userName, int pageNum);
    }
}