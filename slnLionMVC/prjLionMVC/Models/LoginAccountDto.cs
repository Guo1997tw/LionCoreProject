namespace prjLionMVC.Models
{
    public class LoginAccountDto
    {
        public string Account { get; set; } = null!;

        public string HashPassword { get; set; } = null!;

        public string SaltPassword { get; set; } = null!;
    }
}