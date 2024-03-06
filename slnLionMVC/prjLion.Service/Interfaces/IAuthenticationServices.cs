using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Service.Interfaces
{
	public interface IAuthenticationServices
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