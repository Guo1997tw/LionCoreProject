namespace prjLionMVC.Models
{
    public class CreateAccountDto
    {
        public string MemberName { get; set; } = null!;

        public string Account { get; set; } = null!;

        public string HashPassword { get; set; } = null!;

        public string SaltPassword { get; set; } = null!;
    }
}