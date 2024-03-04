using prjLionMVC.Interfaces;
using prjLionMVC.Models;
using prjLionMVC.Models.Entity;
using System.Security.Cryptography;
using System.Text;

namespace prjLionMVC.Implements
{
	public class Lion : ILion
	{
		private readonly LionHwContext _lionHwContext;

		public Lion(LionHwContext lionHwContext)
		{
			_lionHwContext = lionHwContext;
		}

		/// <summary>
		/// 搜尋單一使用者留言
		/// 指定使用者姓名
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public IQueryable<MsgListDto> GetMemberByNameMsg(string userName)
		{
			return _lionHwContext.MessageBoardTables.Join(
				_lionHwContext.MemberTables,
				mb => mb.MemberId,
				m => m.MemberId,
				(mb, m) => new MsgListDto
				{
					MessageBoardId = mb.MessageBoardId,
					MemberName = m.MemberName,
					Account = m.Account,
					MessageText = mb.MessageText,
					MessageTime = mb.MessageTime,
				}).Where(m => m.MemberName == userName);
		}

		/// <summary>
		/// 取得第幾頁
		/// 指定頁面 (分五筆)
		/// </summary>
		/// <param name="pageNum"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public IEnumerable<MsgListDto> GetMsgPageNum(int pageNum)
		{
			int pageNow = 0;
			int pageSize = 5;

			if (pageNum > 0) { pageNow = (pageNum - 1) * pageSize; }

			return _lionHwContext.MessageBoardTables.Join(
				_lionHwContext.MemberTables,
				mb => mb.MemberId,
				m => m.MemberId,
				(mb, m) => new MsgListDto
				{
					MessageBoardId = mb.MessageBoardId,
					MemberName = m.MemberName,
					Account = m.Account,
					MessageText = mb.MessageText,
					MessageTime = mb.MessageTime,
				}).Skip(pageNow).Take(pageSize);
		}

		/// <summary>
		/// 取得留言總筆數
		/// </summary>
		/// <returns></returns>
		public int GetMsgPageCount()
		{
			return _lionHwContext.MessageBoardTables.Count();
		}

		/// <summary>
		/// 註冊帳號
		/// </summary>
		/// <param name="createAccountDto"></param>
		/// <returns></returns>
		public bool CreateMember(CreateAccountDto createAccountDto)
		{
			var salt = RandomSalt();
			var hasPwd = HashPwdWithHMACSHA256(createAccountDto.HashPassword, salt);

			var mapper = new MemberTable
			{
				MemberName = createAccountDto.MemberName,
				Account = createAccountDto.Account,
				HashPassword = hasPwd,
				SaltPassword = salt
			};

			try
			{
				_lionHwContext.MemberTables.Add(mapper);
				_lionHwContext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return true;
		}

		/// <summary>
		/// 登入帳號
		/// </summary>
		/// <param name="loginAccountDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool CheckMember(string account, string password)
		{
			var queryResult = _lionHwContext.MemberTables.FirstOrDefault(m => m.Account == account);

			if (queryResult != null)
			{
				var HashPasswordTemp = queryResult.HashPassword;
				var SaltPasswordTemp = queryResult.SaltPassword;
				var HashPassword = HashPwdWithHMACSHA256(password, SaltPasswordTemp);

				return HashPassword == HashPasswordTemp;
			}

			return false;
		}

		/// <summary>
		/// 取得單一會員資料
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public GetMemberDto GetMemberById(string account)
		{
			var queryResult = _lionHwContext.MemberTables.FirstOrDefault(m => m.Account == account);

			return new GetMemberDto
			{
				MemberId = queryResult.MemberId,
				Account = queryResult.Account
			};
		}

		/// <summary>
		/// 新增留言
		/// </summary>
		/// <param name="createMsgDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool InsertMsg(CreateMsgDto createMsgDto)
		{
			var mapper = new MessageBoardTable
			{
				MemberId = createMsgDto.MemberId,
				MessageText = createMsgDto.MessageText,
				MessageTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"))
			};

			try
			{
				_lionHwContext.MessageBoardTables.Add(mapper);
				_lionHwContext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return true;
		}

		/// <summary>
		/// 修改留言
		/// 指定留言編號 (流水號)
		/// </summary>
		/// <param name="editMsgDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool EditMsg(int id, EditMsgDto editMsgDto)
		{
			var queryResult = _lionHwContext.MessageBoardTables.FirstOrDefault(mb => mb.MessageBoardId == id);

			if (queryResult != null)
			{
				queryResult.MessageText = editMsgDto.MessageText;
				queryResult.MessageTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"));


                _lionHwContext.SaveChanges();

				return true;
			}
			return false;
		}

		/// <summary>
		/// 刪除留言
		/// 指定留言編號 (流水號)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool DeleteMsg(int id)
		{
			var queryResult = _lionHwContext.MessageBoardTables.FirstOrDefault(mb => mb.MessageBoardId == id);

			if (queryResult == null) return false;

			_lionHwContext.Remove(queryResult);
			_lionHwContext.SaveChanges();

			return true;
		}

		/// <summary>
		/// 亂數產生大小
		/// </summary>
		/// <param name="minNum"></param>
		/// <param name="maxNum"></param>
		/// <returns></returns>
		private int RandomNumberSize(int minNum, int maxNum)
		{
			byte[] intBytes = new byte[4];

			RandomNumberGenerator.Fill(intBytes);

			int randomInt = BitConverter.ToInt32(intBytes, 0);

			return Math.Abs(randomInt % (maxNum - minNum)) + minNum;
		}

		/// <summary>
		/// 亂數產生鹽值
		/// </summary>
		/// <param name="minNum"></param>
		/// <param name="maxNum"></param>
		/// <returns></returns>
		private string RandomSalt(int minNum = 8, int maxNum = 256)
		{
			int size = RandomNumberSize(minNum, maxNum);
			var buffer = new byte[size];

			RandomNumberGenerator.Fill(buffer);

			return Convert.ToBase64String(buffer);
		}

		/// <summary>
		/// 密碼雜湊 & 鹽值
		/// </summary>
		/// <param name="password"></param>
		/// <param name="salt"></param>
		/// <returns></returns>
		private string HashPwdWithHMACSHA256(string password, string salt)
		{
			var saltBytes = Convert.FromBase64String(salt);

			using (var hmac = new HMACSHA256(saltBytes))
			{
				var pwdBytes = Encoding.UTF8.GetBytes(password);
				var hash = hmac.ComputeHash(pwdBytes);

				return Convert.ToBase64String(hash);
			}
		}
	}
}