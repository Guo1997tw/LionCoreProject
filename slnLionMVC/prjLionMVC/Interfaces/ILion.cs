using prjLionMVC.Models;

namespace prjLionMVC.Interfaces
{
    public interface ILion
    {
        public IQueryable<MsgListDto> GetMemberByNameMsg(string userName);

        /// <summary>
        /// 取得第幾頁
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public IEnumerable<MsgListDto> GetMsgPageNum(int pageNum);

		/// <summary>
		/// 取得總筆數
		/// </summary>
		/// <returns></returns>
		public int GetMsgPageCount();

        public bool CreateMember(CreateAccountDto createAccountDto);

        public bool CheckMember(string account, string password);

        public GetMemberDto GetMemberById(string account);

        public bool InsertMsg(CreateMsgDto createMsgDto);

        public bool EditMsg(int id, EditMsgDto editMsgDto);

        public bool DeleteMsg(int id);
    }
}