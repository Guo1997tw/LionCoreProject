namespace prjLionMVC.Interfaces
{
    public interface IHttpClientFunctions
    {
		/// <summary>
		/// MVC後端呼叫RequestMethod
		/// </summary>
		/// <typeparam name="InputDataModel"></typeparam>
		/// <typeparam name="OutputDataModel"></typeparam>
		/// <param name="httpMethod"></param>
		/// <param name="apiUrl"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public Task<OutputDataModel> RequestMethod<InputDataModel, OutputDataModel>(HttpMethod httpMethod, string apiUrl, InputDataModel data);
    }
}