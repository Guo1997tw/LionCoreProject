using prjLionMVC.Models;

namespace prjLionMVC.Interfaces
{
    public interface ILion
    {
        public IEnumerable<MsgListDto> GetAllMsg();

        public IQueryable<MsgListDto> GetMemberByNameMsg(string userName);

        public IEnumerable<MsgListDto> GetMsgPage(int choosePage);

        public bool CreateMember(CreateAccountDto createAccountDto);

        public bool CheckMember(string account, string password);

        public GetMemberDto GetMemberById(string account);

        public bool InsertMsg(CreateMsgDto createMsgDto);

        public bool EditMsg(int id, EditMsgDto editMsgDto);

        public bool DeleteMsg(int id);
    }
}