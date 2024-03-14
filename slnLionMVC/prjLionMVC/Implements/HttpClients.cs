using Microsoft.AspNetCore.Mvc;
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
        public async Task<string> MsgPageAllPostAsync(int currentShowPage)
        {
            return await _httpClientFunctions.BuilderGetDataListAsync($"GetPaginationCountDataAll/{currentShowPage}");
        }

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentShowPage"></param>
        /// <returns></returns>
        public async Task<string> SearchMsgUserPostAsync(string userName, int currentShowPage)
        {
            return await _httpClientFunctions.BuilderGetDataListAsync($"GetMsgByUserNamePaginationCountDataAll/{userName}/{currentShowPage}");
        }

        /// <summary>
        /// 註冊帳號頁面
        /// </summary>
        /// <param name="registerMemberViewModel"></param>
        /// <returns></returns>

        public async Task<bool> RegisterPostAsync(RegisterMemberViewModel registerMemberViewModel)
        {
            var json = JsonSerializer.Serialize(registerMemberViewModel);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClientFunctions.BuilderPostDataListAsync("RegisterMember", content);
        }

        /// <summary>
        /// 登入帳號頁面
        /// </summary>
        /// <param name="loginMemberViewModel"></param>
        /// <returns></returns>
        public async Task<ResultTLoginInfoViewModel<LoginInfoViewModel?>> LoginPostAsync(LoginMemberViewModel loginMemberViewModel)
        {
            var json = JsonSerializer.Serialize(loginMemberViewModel);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var queryResult = await _httpClientFunctions.BuilderGetAccountAsync("LoginMember", content);

            if(queryResult != "false")
            {
                var jsonResult = JsonSerializer.Deserialize<ResultTLoginInfoViewModel<LoginInfoViewModel>>(queryResult);

                return new ResultTLoginInfoViewModel<LoginInfoViewModel?>
                {
                    data = jsonResult.data
                };
            }
            else
            {
                return new ResultTLoginInfoViewModel<LoginInfoViewModel?>
                {
                    ErrorMessage = "false"
                };
            }
        }

        /// <summary>
        /// 新增留言頁面
        /// </summary>
        /// <param name="insertMsgViewModel"></param>
        /// <returns></returns>
        public async Task<bool> UseMsgPostAsync(InsertMsgViewModel insertMsgViewModel)
        {
            var json = JsonSerializer.Serialize(insertMsgViewModel);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClientFunctions.BuilderPostDataListAsync("CreateUserMsg", content);
        }

        /// <summary>
        /// 編輯留言頁面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editMsgViewModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> EditMsgPostAsync(int id, EditMsgViewModel editMsgViewModel)
        {
            var json = JsonSerializer.Serialize(editMsgViewModel);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _httpClientFunctions.BuilderPutDataListAsync($"UpdateUserMsg/{id}", content);
        }

        /// <summary>
        /// 刪除留言
        /// 指定留言編號 (流水號)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveMsgPostAsync(int id)
        {
            return await _httpClientFunctions.BuilderDeleteDataAsync($"RemoveMemberMsg/{id}");
        }
    }
}