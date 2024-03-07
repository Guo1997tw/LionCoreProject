namespace prjLion.WebAPI.Models.HttpClients.Out
{
	public class LoginInfoViewModel
	{
		public int MemberId { get; set; }

		public string Account { get; set; } = null!;

		public string HashPassword { get; set; } = null!;
	}
}