namespace prjLionMVC.Models
{
    public class CreateMsgDto
    {
        public int MemberId { get; set; }

        public string MessageText { get; set; } = null!;

        public DateTime MessageTime { get; set; }
    }
}