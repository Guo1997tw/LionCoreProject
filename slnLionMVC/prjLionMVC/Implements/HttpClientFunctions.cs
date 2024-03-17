using Azure;
using Azure.Core;
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

        public HttpClientFunctions(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

		/// <summary>
		/// MVC後端呼叫RequestMethod
		/// </summary>
		/// <typeparam name="InputDataModel"></typeparam>
		/// <typeparam name="OutputDataModel"></typeparam>
		/// <param name="httpMethod"></param>
		/// <param name="apiUrl"></param>
		/// <param name="inputDataModel"></param>
		/// <returns></returns>
		public async Task<OutputDataModel> RequestMethod<InputDataModel, OutputDataModel>(HttpMethod httpMethod, string apiUrl, InputDataModel inputDataModel)
        {
			var jsonAutoConvert = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

			var client = _httpClientFactory.CreateClient();

            var json = JsonSerializer.Serialize(inputDataModel);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage httpResponseMessage = null!;
                switch (httpMethod)
                {
                    case HttpMethod when httpMethod == HttpMethod.Post:
                        httpResponseMessage = await client.PostAsync(apiUrl, content);
                        break;

                    case HttpMethod when httpMethod == HttpMethod.Put:
                        httpResponseMessage = await client.PutAsync(apiUrl, content);
                        break;

                    case HttpMethod when httpMethod == HttpMethod.Delete:
                        httpResponseMessage = await client.DeleteAsync(apiUrl);
                        break;
                }

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentResult = await httpResponseMessage.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<OutputDataModel>(contentResult, jsonAutoConvert);
                }
                else
                {
                    return default;
					// throw new ApplicationException($"對 {apiUrl} 失敗，狀態碼為: {httpResponseMessage.StatusCode}。");
				}
                
            }
            catch(HttpRequestException ex)
            {
				throw new ApplicationException("發送請求Http錯誤", ex);
			}
        }
    }
}