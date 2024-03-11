using Microsoft.AspNetCore.Mvc;

namespace prjLionMVC.Interfaces
{
    public interface IHttpClients
    {
        /// <summary>
        /// 分頁功能
        /// </summary>
        /// <returns></returns>
        public Task<string> MsgPageAllPostAsync(int currentShowPage);
    }
}