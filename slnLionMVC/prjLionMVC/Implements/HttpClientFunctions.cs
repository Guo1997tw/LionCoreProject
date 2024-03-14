using Microsoft.Extensions.Options;
using prjLionMVC.Interfaces;
using prjLionMVC.Models.Infrastructures;
using System.Net.Http;
using System.Text.Json;

namespace prjLionMVC.Implements
{
    public class HttpClientFunctions : IHttpClientFunctions
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LionApiSettings _lionApiSettings;

        public HttpClientFunctions(IHttpClientFactory httpClientFactory, IOptions<LionApiSettings> lionApiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _lionApiSettings = lionApiSettings.Value;
        }

        /// <summary>
        /// 刪除留言
        /// 指定留言編號 (流水號)
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