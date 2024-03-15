using Microsoft.AspNetCore.Mvc;
using prjLionMVC.Models.HttpClients;
using prjLionMVC.Models.HttpClients.Inp;
using prjLionMVC.Models.HttpClients.Out;

namespace prjLionMVC.Interfaces
{
    public interface IHttpClients
    {
        /// <summary>
        /// 同時取得資料分頁與總筆數
        /// 指定頁數
        /// </summary>
        /// <returns></returns>
        public Task<ResultTOutputDataViewModel<PaginationCountDataViewModel>> MsgPageAllPostAsync(int currentShowPage);

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentShowPage"></param>
        /// <returns></returns>
        public Task<ResultTOutputDataViewModel<PaginationCountDataViewModel>> SearchMsgUserPostAsync(string userName, int currentShowPage);

        /// <summary>
        /// 註冊帳號頁面
        /// </summary>
        /// <param name="registerMemberViewModel"></param>
        /// <returns></returns>
        public Task<ResultTOutputDataViewModel<PaginationCountDataViewModel>> RegisterPostAsync(RegisterMemberViewModel registerMemberViewModel);

        /// <summary>
        /// 登入帳號頁面
        /// </summary>
        /// <param name="loginMemberViewModel"></param>
        /// <returns></returns>
        public Task<ResultTLoginInfoViewModel<LoginInfoViewModel?>> LoginPostAsync(LoginMemberViewModel loginMemberViewModel);

        /// <summary>
        /// 新增留言頁面
        /// </summary>
        /// <param name="insertMsgViewModel"></param>
        /// <returns></returns>
        public Task<bool> UseMsgPostAsync(InsertMsgViewModel insertMsgViewModel);


        /// <summary>
        /// 編輯留言頁面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editMsgViewModel"></param>
        /// <returns></returns>
        public Task<bool> EditMsgPostAsync(int id, EditMsgViewModel editMsgViewModel);

        /// <summary>
        /// 刪除留言
        /// 指定留言編號 (流水號)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> RemoveMsgPostAsync(int id);
    }
}