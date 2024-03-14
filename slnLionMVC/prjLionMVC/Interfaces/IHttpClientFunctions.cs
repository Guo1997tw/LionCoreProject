namespace prjLionMVC.Interfaces
{
    public interface IHttpClientFunctions
    {
        /// <summary>
        /// 取資料動作
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <returns></returns>
        public Task<string> BuilderGetDataListAsync(string apiMethod);

        /// <summary>
        /// 取帳號動作
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task<string> BuilderGetAccountAsync(string apiMethod, StringContent content);

        /// <summary>
        /// 新增動作
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task<bool> BuilderPostDataListAsync(string apiMethod, StringContent content);

        /// <summary>
        /// 編輯動作
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task<bool> BuilderPutDataListAsync(string apiMethod, StringContent content);

        /// <summary>
        /// 刪除動作
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <returns></returns>
        public Task<bool> BuilderDeleteDataAsync(string apiMethod);
    }
}