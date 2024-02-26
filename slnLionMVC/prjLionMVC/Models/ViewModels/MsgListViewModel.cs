namespace prjLionMVC.Models.ViewModels
{
    public class MsgListViewModel
    {
        public int MessageBoardId { get; set; }

        public string MemberName { get; set; } = null!;

        public string Account { get; set; } = null!;

        public string MessageText { get; set; } = null!;

        public DateTime MessageTime { get; set; }
    }
}