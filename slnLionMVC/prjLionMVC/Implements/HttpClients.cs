using Microsoft.AspNetCore.Mvc;
using prjLionMVC.Interfaces;
using System.Net.Http;

namespace prjLionMVC.Implements
{
    public class HttpClients : IHttpClients
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClients(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> MsgPageAllPostAsync(int currentShowPage)
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                var respone = await client.PostAsync($"https://localhost:7235/api/Lion/GetPaginationCountDataAll/{currentShowPage}", null);

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
    }
}