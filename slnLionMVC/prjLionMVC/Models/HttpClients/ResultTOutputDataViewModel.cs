namespace prjLionMVC.Models.HttpClients
{
    public class ResultTOutputDataViewModel<T>
    {
        public bool success { get; set; }

        public string message { get; set; } = null!;

        public T? data { get; set; }
    }
}