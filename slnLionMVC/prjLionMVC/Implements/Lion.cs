using prjLionMVC.Interfaces;
using prjLionMVC.Models;
using prjLionMVC.Models.Entity;

namespace prjLionMVC.Implements
{
    public class Lion : ILion
    {
        private readonly LionHwContext _lionHwContext;

        public Lion(LionHwContext lionHwContext)
        {
            _lionHwContext = lionHwContext;
        }

        public IEnumerable<MsgListDto> GetAllMsg()
        {
            return _lionHwContext.MessageBoardTables.Join(
                _lionHwContext.MemberTables,
                mb => mb.MemberId,
                m => m.MemberId,
                (mb, m) => new MsgListDto
                {
                    MessageBoardId = mb.MemberId,
                    MemberName = m.MemberName,
                    Account = m.Account,
                    MessageText = mb.MessageText,
                    MessageTime = mb.MessageTime,
                });
        }
    }
}