using AutoMapper;
using prjLion.Service.Models.Bo;
using prjLion.WebAPI.Models;
using prjLion.WebAPI.Models.HttpClients.Out;

namespace prjLion.WebAPI.Mapping
{
    public class PresentationProfile : Profile
    {
        public PresentationProfile()
        {
            // 搜尋單一使用者留言
            // 指定使用者姓名
            CreateMap<MessageListBo, MessageListViewModel>();

            // 註冊帳號
            CreateMap<MemberAccountViewModel, MemberAccountBo>();

            // 登入帳號
            CreateMap<MemberAccountBo, LoginInfoViewModel>();
        }
    }
}