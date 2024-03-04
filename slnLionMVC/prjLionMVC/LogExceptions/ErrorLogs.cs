using prjLionMVC.Models.Entity;

namespace prjLionMVC.LogExceptions
{
    public class ErrorLogs
    {
        private readonly RequestDelegate _requestDelegate;

        public ErrorLogs(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        /// <summary>
        /// 拋出例外 (錯誤) 訊息寫入資料庫存取
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="lionHwContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext, LionHwContext lionHwContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch(Exception ex)
            {
                var errorlog = new ErrorLogTable
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.UtcNow,
                };

                lionHwContext.ErrorLogTables.Add(errorlog);

                await lionHwContext.SaveChangesAsync();

                throw;
            }
        }
    }
}