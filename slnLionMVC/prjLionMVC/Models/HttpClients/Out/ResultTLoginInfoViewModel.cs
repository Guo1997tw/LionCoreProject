namespace prjLionMVC.Models.HttpClients.Out
{
    /// <summary>
    /// 反序列化資料都需要與JSON格式一樣小寫
    /// </summary>
    public class ResultTLoginInfoViewModel<LoginInfoViewModel>
    {
        public LoginInfoViewModel? data { get; set; }

        public string ErrorMessage { get; set; } = null!;
    }
}