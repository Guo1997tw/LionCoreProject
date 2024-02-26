namespace prjLionMVC.Models.ViewModels
{
    public class CreateAccountViewModel
    {
        public string MemberName { get; set; } = null!;

        public string Account { get; set; } = null!;

        public string HashPassword { get; set; } = null!;
    }
}