namespace prjLion.WebAPI.Models
{
    public class ResultTViewModel<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = null!;

        public T? Data { get; set; }
    }
}