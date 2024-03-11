namespace prjLion.WebAPI.Models
{
	public class ResultLoginViewModel
	{
		public bool Success { get; set; }

		public string Message { get; set; } = null!;

		public int memberId { get; set; }

		public string account { get; set; } = null!;
	}
}