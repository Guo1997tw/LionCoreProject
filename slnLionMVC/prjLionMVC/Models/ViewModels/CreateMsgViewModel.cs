namespace prjLionMVC.Models.ViewModels
{
    public class CreateMsgViewModel
    {
        public int MemberId { get; set; }

        public string MessageText { get; set; } = null!;

        public DateTime MessageTime { get; set; }
    }
}