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

        public async Task<string> RegisterPostAsync(RegisterMemberViewModel registerMemberViewModel)
        {
            var client = _httpClientFactory.CreateClient();

            var json = JsonSerializer.Serialize(registerMemberViewModel);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/RegisterMember", content);

                return (response.IsSuccessStatusCode) ? ("true") : ("false");
            }
            catch (HttpRequestException)
            {
                return "false";
            }
        }

        /// <summary>
        /// 登入帳號頁面
        /// </summary>
        /// <param name="loginMemberViewModel"></param>
        /// <returns></returns>
        public async Task<ResultTLoginInfoViewModel<LoginInfoViewModel?>> LoginPostAsync(LoginMemberViewModel loginMemberViewModel)
        {
            // 建立連線
            var client = _httpClientFactory.CreateClient();

            // 序列化
            var json = JsonSerializer.Serialize(loginMemberViewModel);

            // 指定ContentType
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/LoginMember", content);

                if (response.IsSuccessStatusCode)
                {
                    // 讀取資料
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // 反序列化
                    var queryResult = JsonSerializer.Deserialize<ResultTLoginInfoViewModel<LoginInfoViewModel>>(responseContent);

                    return new ResultTLoginInfoViewModel<LoginInfoViewModel?>
                    {
                        data = queryResult.data,
                    };
                }
                else
                {
                    return new ResultTLoginInfoViewModel<LoginInfoViewModel?> { ErrorMessage = "false" };
                }
            }
            catch(HttpRequestException)
            {
                return new ResultTLoginInfoViewModel<LoginInfoViewModel?> { ErrorMessage = "false" };
            }
        }

        /// <summary>
        /// 新增留言頁面
        /// </summary>
        /// <param name="insertMsgViewModel"></param>
        /// <returns></returns>
        public async Task<string> UseMsgPostAsync(InsertMsgViewModel insertMsgViewModel)
        {
            var client = _httpClientFactory.CreateClient();

            var json = JsonSerializer.Serialize(insertMsgViewModel);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var respone = await client.PostAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/CreateUserMsg", content);

                return (respone.IsSuccessStatusCode) ? ("true") : ("false");
            }
            catch (HttpRequestException)
            {
                return ("false");
            }
        }

        /// <summary>
        /// 編輯留言頁面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editMsgViewModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> EditMsgPostAsync(int id, EditMsgViewModel editMsgViewModel)
        {
            var client = _httpClientFactory.CreateClient();

            var json = JsonSerializer.Serialize(editMsgViewModel);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var respone = await client.PutAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/UpdateUserMsg/{id}", content);

                return (respone.IsSuccessStatusCode) ? ("true") : ("false");
            }
            catch (HttpRequestException)
            {
                return ("false");
            }
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