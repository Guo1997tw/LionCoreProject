﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using prjLionMVC.Interfaces;
using prjLionMVC.Models.HttpClients.Inp;
using prjLionMVC.Models.Infrastructures;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Security.Claims;
using prjLionMVC.Models.HttpClients.Out;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using prjLionMVC.Models.HttpClients;

namespace prjLionMVC.Implements
{
    public class HttpClients : IHttpClients
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserAuthentication _userAuthentication;
        private readonly IHttpClientFunctions _httpClientFunctions;
        private readonly LionApiSettings _lionApiSettings;

        public HttpClients(IHttpClientFactory httpClientFactory, IOptions<LionApiSettings> lionApiSettings, IUserAuthentication userAuthentication, IHttpClientFunctions httpClientFunctions)
        {
            _httpClientFactory = httpClientFactory;
            _userAuthentication = userAuthentication;
            _httpClientFunctions = httpClientFunctions;
            _lionApiSettings = lionApiSettings.Value;
        }

        /// <summary>
        /// 同時取得資料分頁與總筆數
        /// 指定頁數
        /// </summary>
        /// <param name="currentShowPage"></param>
        /// <returns></returns>
        public async Task<ResultTOutputDataViewModel<PaginationCountDataViewModel>> MsgPageAllPostAsync(int currentShowPage)
        {
            return await _httpClientFunctions.RequestMethod<string, ResultTOutputDataViewModel<PaginationCountDataViewModel>>(HttpMethod.Post, $"{_lionApiSettings.LionBaseUrl}/api/Lion/GetPaginationCountDataAll/{currentShowPage}", null);
        }

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentShowPage"></param>
        /// <returns></returns>
        public async Task<ResultTOutputDataViewModel<PaginationCountDataViewModel>> SearchMsgUserPostAsync(string userName, int currentShowPage)
        {
            return await _httpClientFunctions.RequestMethod<string, ResultTOutputDataViewModel<PaginationCountDataViewModel>>(HttpMethod.Post, $"{_lionApiSettings.LionBaseUrl}/api/Lion/GetMsgByUserNamePaginationCountDataAll/{userName}/{currentShowPage}", null);
        }

        /// <summary>
        /// 註冊帳號頁面
        /// </summary>
        /// <param name="registerMemberViewModel"></param>
        /// <returns></returns>

        public async Task<ResultTOutputDataViewModel<PaginationCountDataViewModel>> RegisterPostAsync(RegisterMemberViewModel registerMemberViewModel)
        {
            return await _httpClientFunctions.RequestMethod<RegisterMemberViewModel, ResultTOutputDataViewModel<PaginationCountDataViewModel>>(HttpMethod.Post, $"{_lionApiSettings.LionBaseUrl}/api/Lion/RegisterMember", registerMemberViewModel);
        }

        /// <summary>
        /// 登入帳號頁面
        /// </summary>
        /// <param name="loginMemberViewModel"></param>
        /// <returns></returns>
        public async Task<ResultTOutputDataViewModel<LoginInfoViewModel?>> LoginPostAsync(LoginMemberViewModel loginMemberViewModel)
        {
            var result = await _httpClientFunctions.RequestMethod<LoginMemberViewModel, ResultTOutputDataViewModel<LoginInfoViewModel?>>(HttpMethod.Post, $"{_lionApiSettings.LionBaseUrl}/api/Lion/LoginMember", loginMemberViewModel);

            if(result != null)
            {
                return new ResultTOutputDataViewModel<LoginInfoViewModel?>
                {
                    data = result.data,
                };
			}
            else
            {
                return new ResultTOutputDataViewModel<LoginInfoViewModel?>
                {
                    message = "false"
                };
            }
        }

        /// <summary>
        /// 新增留言頁面
        /// </summary>
        /// <param name="insertMsgViewModel"></param>
        /// <returns></returns>
        public async Task<ResultTOutputDataViewModel<ResultMsgViewModel>> UseMsgPostAsync(InsertMsgViewModel insertMsgViewModel)
        {
            return await _httpClientFunctions.RequestMethod<InsertMsgViewModel, ResultTOutputDataViewModel<ResultMsgViewModel>>(HttpMethod.Post, $"{_lionApiSettings.LionBaseUrl}/api/Lion/CreateUserMsg", insertMsgViewModel);
        }

        /// <summary>
        /// 編輯留言頁面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editMsgViewModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultTOutputDataViewModel<ResultMsgViewModel>> EditMsgPostAsync(int id, EditMsgViewModel editMsgViewModel)
        {
            return await _httpClientFunctions.RequestMethod<EditMsgViewModel, ResultTOutputDataViewModel<ResultMsgViewModel>>(HttpMethod.Put, $"{_lionApiSettings.LionBaseUrl}/api/Lion/UpdateUserMsg/{id}", editMsgViewModel);
        }

        /// <summary>
        /// 刪除留言
        /// 指定留言編號 (流水號)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultTOutputDataViewModel<int>> RemoveMsgPostAsync(int id)
        {
            return await _httpClientFunctions.RequestMethod<string, ResultTOutputDataViewModel<int>>(HttpMethod.Delete, $"{_lionApiSettings.LionBaseUrl}/api/Lion/RemoveMemberMsg/{id}", null);
        }
    }
}