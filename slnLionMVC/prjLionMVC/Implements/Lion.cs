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
		/// 留言版清單
		/// </summary>
		/// <returns></returns>
		public IEnumerable<MsgListDto> GetAllMsg()
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
				});
		}

		/// <summary>
		/// 搜尋單一使用者
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
		/// 清單分頁
		/// </summary>
		/// <param name="choosePage"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public IEnumerable<MsgListDto> GetMsgPage(int choosePage)
		{
			int pageNow = 0;
			int pageSize = 5;

			if (choosePage > 0)
			{
				pageNow = (choosePage - 1) * pageSize;
			}

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
		/// 建立帳號
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
		/// 會員登入
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
		/// 留言版新增
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
				MessageTime = DateTime.UtcNow
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
		/// 留言版編輯
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
				queryResult.MessageTime = DateTime.UtcNow;

				_lionHwContext.SaveChanges();

				return true;
			}
			return false;
		}

		/// <summary>
		/// 留言版刪除
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