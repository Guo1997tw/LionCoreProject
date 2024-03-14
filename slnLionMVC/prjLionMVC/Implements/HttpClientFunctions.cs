﻿using Azure;
using Microsoft.Extensions.Options;
using prjLionMVC.Interfaces;
using prjLionMVC.Models.HttpClients.Inp;
using prjLionMVC.Models.Infrastructures;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace prjLionMVC.Implements
{
    public class HttpClientFunctions : IHttpClientFunctions
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LionApiSettings _lionApiSettings;

        /// <summary>
        /// 取資料動作
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="lionApiSettings"></param>
        public HttpClientFunctions(IHttpClientFactory httpClientFactory, IOptions<LionApiSettings> lionApiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _lionApiSettings = lionApiSettings.Value;
        }

        public async Task<string> BuilderGetDataListAsync(string apiMethod)
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                var respone = await client.PostAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/{apiMethod}", null);

                if(respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsStringAsync();

                    return content;
                }
                else
                {
                    return "false";
                }
            }
            catch(HttpRequestException)
            {
                return "false";
            }
        }

        /// <summary>
        /// 取帳號動作
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<string> BuilderGetAccountAsync(string apiMethod, StringContent content)
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                var respone = await client.PostAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/{apiMethod}", content);

                if (respone.IsSuccessStatusCode)
                {
                    var contentResult = await respone.Content.ReadAsStringAsync();

                    return contentResult;
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

        public async Task<bool> BuilderPostDataListAsync(string apiMethod, StringContent content)
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.PostAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/{apiMethod}", content);

                return (response.IsSuccessStatusCode) ? true : false;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        /// <summary>
        /// 編輯動作
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<bool> BuilderPutDataListAsync(string apiMethod, StringContent content)
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.PutAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/{apiMethod}", content);

                return (response.IsSuccessStatusCode) ? true : false;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        /// <summary>
        /// 刪除動作
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <returns></returns>
        public async Task<bool> BuilderDeleteDataAsync(string apiMethod)
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                var respone = await client.DeleteAsync($"{_lionApiSettings.LionBaseUrl}/api/Lion/{apiMethod}");

                return (respone.IsSuccessStatusCode) ? true : false;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}