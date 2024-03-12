﻿using Microsoft.AspNetCore.Mvc;
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
        public Task<string> MsgPageAllPostAsync(int currentShowPage);

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentShowPage"></param>
        /// <returns></returns>
        public Task<string> SearchMsgUserPostAsync(string userName, int currentShowPage);

        /// <summary>
        /// 註冊帳號頁面
        /// </summary>
        /// <param name="registerMemberViewModel"></param>
        /// <returns></returns>
        public Task<string> RegisterPostAsync(RegisterMemberViewModel registerMemberViewModel);

        /// <summary>
        /// 登入帳號頁面
        /// </summary>
        /// <param name="loginMemberViewModel"></param>
        /// <returns></returns>
        public Task<LoginInfoViewModel?> LoginPostAsync(LoginMemberViewModel loginMemberViewModel);

        /// <summary>
        /// 新增留言頁面
        /// </summary>
        /// <param name="insertMsgViewModel"></param>
        /// <returns></returns>
        public Task<string> UseMsgPostAsync(InsertMsgViewModel insertMsgViewModel);
    }
}