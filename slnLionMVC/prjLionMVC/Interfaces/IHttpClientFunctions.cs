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
        /// 刪除動作
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <returns></returns>
        public Task<bool> BuilderDeleteDataAsync(string apiMethod);
    }
}