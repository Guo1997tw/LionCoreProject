namespace prjLionMVC.Models
{
    public class MsgListDto
    {
        public int MessageBoardId { get; set; }

        public string MemberName { get; set; } = null!;

        public string Account { get; set; } = null!;

        public string MessageText { get; set; } = null!;

        public DateTime MessageTime { get; set; }
    }
}