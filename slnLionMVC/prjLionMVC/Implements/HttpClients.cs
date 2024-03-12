﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using prjLionMVC.Interfaces;
using prjLionMVC.Models.HttpClients.Inp;
using prjLionMVC.Models.Infrastructures;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace prjLionMVC.Implements
{
    public class HttpClients : IHttpClients
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LionApiSettings _lionApiSettings;

        public HttpClients(IHttpClientFactory httpClientFactory, IOptions<LionApiSettings> lionApiSettings)
        {
            _httpClientFactory = httpClientFactory;
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
            var client = _httpClientFactory.CreateClient();

            try
            {
                var respone = await client.PostAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/GetPaginationCountDataAll/{currentShowPage}", null);

                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsStringAsync();

                    return content;
                }
                else
                {
                    return "false";
                }
            }
            catch (HttpRequestException)
            {
                return "false";
            }
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
            var client = _httpClientFactory.CreateClient();

            try
            {
                var respone = await client.PostAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/GetMsgByUserNamePaginationCountDataAll/{userName}/{currentShowPage}", null);

                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsStringAsync();

                    return content;
                }
                else
                {
                    return "false";
                }
            }
            catch (HttpRequestException)
            {
                return "false";
            }
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
    }
}