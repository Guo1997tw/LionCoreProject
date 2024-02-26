using prjLionMVC.Models;

namespace prjLionMVC.Interfaces
{
    public interface ILion
    {
        public IEnumerable<MsgListDto> GetAllMsg();
    }
}