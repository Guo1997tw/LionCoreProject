namespace prjLionMVC.Interfaces
{
	public interface IUserSession
	{
		/// <summary>
		/// 取得Session使用者ID
		/// </summary>
		/// <returns></returns>
		public int GetSessionCertificate();

		/// <summary>
		/// 取得Session使用者名稱
		/// </summary>
		/// <returns></returns>
		public string GetSessionUserName();
	}
}