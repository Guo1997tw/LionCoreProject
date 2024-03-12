using prjLionMVC.Models.HttpClients.Inp;

namespace prjLionMVC.Interfaces
{
    public interface IHttpClientlogics
    {
        public Task<string> IsIdentityCheckAsync(LoginMemberViewModel loginMemberViewModel);
    }
}