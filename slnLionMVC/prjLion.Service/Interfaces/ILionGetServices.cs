using prjLion.Service.Models.Bo;

namespace prjLion.Service.Interfaces
{
    public interface ILionGetServices
    {
        /// <summary>
        /// 同時取得資料分頁與總筆數
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public Task<PaginationCountBo<MessageListBo>?> GetPaginationCountData(int pageNum);

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public Task<PaginationCountBo<MessageListBo>?> GetMsgByUserNamePaginationCountData(string userName, int pageNum);
    }
}