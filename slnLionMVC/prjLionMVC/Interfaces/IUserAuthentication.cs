namespace prjLionMVC.Interfaces
{
    public interface IUserAuthentication
    {
        /// <summary>
        /// 取得使用者識別碼
        /// 會員編號
        /// </summary>
        /// <returns></returns>
        public int GetUserCertificate();

        /// <summary>
        /// 取得使用者名稱
        /// 會員帳號
        /// </summary>
        /// <returns></returns>
        public string GetUserName();
    }
}