namespace prjLionMVC.Interfaces
{
    public interface IHttpClientFunctions
    {
        public Task<string> BuilderGetDataListAsync(string apiMethod);

        /// <summary>
        /// 刪除留言
        /// 指定留言編號 (流水號)
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <returns></returns>
        public Task<bool> BuilderDeleteDataAsync(string apiMethod);
    }
}