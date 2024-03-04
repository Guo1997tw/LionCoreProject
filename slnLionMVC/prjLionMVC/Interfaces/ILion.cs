using prjLionMVC.Models;

namespace prjLionMVC.Interfaces
{
    public interface ILion
    {
		/// <summary>
		/// 搜尋單一使用者留言
		/// 指定使用者姓名
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public IQueryable<MsgListDto> GetMemberByNameMsg(string userName);

		/// <summary>
		/// 取得第幾頁
		/// 指定頁面 (分五筆)
		/// </summary>
		/// <param name="pageNum"></param>
		/// <returns></returns>
		public IEnumerable<MsgListDto> GetMsgPageNum(int pageNum);

		/// <summary>
		/// 取得留言總筆數
		/// </summary>
		/// <returns></returns>
		public int GetMsgPageCount();

		/// <summary>
		/// 註冊帳號
		/// </summary>
		/// <param name="createAccountDto"></param>
		/// <returns></returns>
		public bool CreateMember(CreateAccountDto createAccountDto);

		/// <summary>
		/// 登入帳號
		/// </summary>
		/// <param name="account"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public bool CheckMember(string account, string password);

		/// <summary>
		/// 取得單一使用者資訊
		/// 指定帳號
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		public GetMemberDto GetMemberById(string account);

		/// <summary>
		/// 新增留言
		/// </summary>
		/// <param name="createMsgDto"></param>
		/// <returns></returns>
		public bool InsertMsg(CreateMsgDto createMsgDto);

		/// <summary>
		/// 修改留言
		/// 指定留言編號 (流水號)
		/// </summary>
		/// <param name="id"></param>
		/// <param name="editMsgDto"></param>
		/// <returns></returns>
		public bool EditMsg(int id, EditMsgDto editMsgDto);

		/// <summary>
		/// 刪除留言
		/// 指定留言編號 (流水號)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool DeleteMsg(int id);
    }
}