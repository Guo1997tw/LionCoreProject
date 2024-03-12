namespace prjLion.WebAPI.Models
{
    public class ResultViewModel
    {
        public bool Success { get; set; }

        public string Message { get; set; } = null!;

        public object Data { get; set; } = null!;
    }
}